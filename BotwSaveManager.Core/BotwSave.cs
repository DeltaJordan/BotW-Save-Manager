#pragma warning disable IDE0063 // Use simple 'using' statement

using BotwSaveManager.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwSaveManager.Core
{
    public enum SaveType : uint {
        WiiU = 0x1B470000,
        Switch = 0x0000471B
    }

    public class BotwSave
    {
        private bool Skip;

        private static readonly ushort[] Headers = new ushort[] {
            0x24e2, 0x24EE, 0x2588, 0x29c0,
            0x3ef8, 0x471a, 0x471b, 0x471e
        };

        private static readonly string[] Versions = new string[] {
            "v1.0", "v1.1", "v1.2", "v1.3",
            "v1.3.3", "v1.4", "v1.5", "v1.6"
        };

        private static readonly string[] Items = new string[] {
            "Item", "Weap", "Armo", "Fire", "Norm", "IceA", "Elec", "Bomb", "Anci", "Anim",
            "Obj_", "Game", "Dm_N", "Dm_A", "Dm_E", "Dm_P", "FldO", "Gano", "Gian", "Grea",
            "KeyS", "Kokk", "Liza", "Mann", "Mori", "Npc_", "OctO", "Octa", "Octa", "arro",
            "Pict", "PutR", "Rema", "Site", "TBox", "TwnO", "Prie", "Dye0", "Dye1", "Map",
            "Play", "Oasi", "Cele", "Wolf", "Gata", "Ston", "Kaka", "Soji", "Hyru", "Powe",
            "Lana", "Hate", "Akka", "Yash", "Dung", "BeeH", "Boar", "Boko", "Brig", "DgnO"
        };

        private static readonly uint[] Hashes = new uint[] {
            0x7B74E117, 0x17E1747B, 0xD913B769, 0x69B713D9, 0xB666D246, 0x46D266B6, 0x021A6FF2,
            0xF26F1A02, 0xFF74960F, 0x0F9674FF, 0x8932285F, 0x5F283289, 0x3B0A289B, 0x9B280A3B,
            0x2F95768F, 0x8F76952F, 0x9C6CFD3F, 0x3FFD6C9C, 0xBBAC416B, 0x6B41ACBB, 0xCCAB71FD,
            0xFD71ABCC, 0xCBC6B5E4, 0xE4B5C6CB, 0x2CADB0E7, 0xE7B0AD2C, 0xA6EB3EF4, 0xF43EEBA6,
            0x21D4CFFA, 0xFACFD421, 0x22A510D1, 0xD110A522, 0x98D10D53, 0x530DD198, 0x55A22047,
            0x4720A255, 0xE5A63A33, 0x333AA6E5, 0xBEC65061, 0x6150C6BE, 0xBC118370, 0x708311BC,
            0x0E9D0E75, 0x750E9D0E
        };

        public SaveType SaveType { get; set; }
        public string SourceFolder { get; set; }
        public List<string> VersionList { get; set; } = new();

        public BotwSave(string sourceFolder, bool skipVersionCheck = false)
        {
            SourceFolder = sourceFolder;

            if (!File.Exists($"{sourceFolder}/option.sav")) {
                throw new FileNotFoundException(
                    $"Invalid BotwSave folder: '{sourceFolder}'\n" +
                    $"The selected folder does not contain an 'option.sav' file."
                );
            }

            // Read save type
            using (BinaryReader reader = new(File.OpenRead($"{sourceFolder}/option.sav"))) {
                SaveType = (SaveType)BitConverter.ToUInt32(reader.ReadBytes(4));
            }

            foreach (var file in Directory.EnumerateFiles(SourceFolder, "game_data.sav", SearchOption.AllDirectories)) {

                try {
                    Logger.Write($"Loading \"{file}\"...");
                    using (FileStream fs = File.OpenRead(file)) {

                        // Read header data
                        byte[] headerData = new byte[4];
                        fs.Read(headerData, 0, 4);

                        // Reverse header on WiiU files
                        if (SaveType == SaveType.WiiU) {
                            Array.Reverse(headerData);
                        }

                        // Collect version
                        string version = Versions[Headers.AsSpan().IndexOf(BitConverter.ToUInt16(headerData))];
                        VersionList.Add($"Save: {Path.GetFileName(Path.GetDirectoryName(file))} - {version}");
                        Logger.Write($"Found {SaveType} version {version} on \"{file}\"");
                    }
                }
                catch (Exception ex) {
                    Logger.Write($"Could not identify version on save '{Path.GetFileName(Path.GetDirectoryName(file))}' | {ex}");
                    if (!skipVersionCheck) {
                        throw;
                    }
                }
            }
        }

        public void ConvertPlatform(string dst)
        {
            if (dst == SourceFolder) {
                Logger.Write($"Backing up source directory...");
                DirectoryHelper.Copy(SourceFolder, $"{SourceFolder}/backup", true);
            }
            else {
                Logger.Write($"Copying save files to the output directory...");
                DirectoryHelper.Copy(SourceFolder, dst, true);
                SourceFolder = dst;
            }

            foreach (var file in Directory.EnumerateFiles(SourceFolder, "*.sav", SearchOption.AllDirectories)) {

                Logger.Write($"Converting {file}...");

                // Read file into memory
                byte[] data = File.ReadAllBytes(file);
                using MemoryStream stream = new(data);
                using BinaryReader reader = new(stream);
                using BinaryWriter writer = new(stream);

                for (int pos = 0; pos < data.Length / 4; pos++) {

                    // Inverse trackblock headers
                    if (file.Contains("trackblock") && pos == 0) {
                        stream.Position = 4;
                        writer.ReverseBuffer(stream, reader.ReadBytes(2));
                        pos = 2;
                    }

                    stream.Position = pos * 4;
                    byte[] buffer = reader.ReadBytes(4);

                    if (Hashes.Contains(BitConverter.ToUInt32(buffer))) {
                        writer.ReverseBuffer(stream, buffer);
                        pos++;
                        Skip = true;
                    }
                    else {
                        Skip = false;
                    }

                    if (!Skip && !Items.Any(x => Encoding.UTF8.GetString(buffer).Contains(x))) {
                        writer.ReverseBuffer(stream, buffer);
                    }
                    else if (Skip == false) {
                        pos++;
                        for (int i = 0; i < 16; i++) {
                            stream.Position = (pos + (i * 2)) * 4;
                            buffer = reader.ReadBytes(4);
                            writer.ReverseBuffer(stream, buffer);
                        }

                        pos += 30; // 0x1E
                    }
                }

                writer.Flush();
                File.WriteAllBytes(file, stream.ToArray());
                Logger.Write($"Successfully converted {file}...");
            }

            SaveType = SaveType.Reverse();
        }
    }
}
