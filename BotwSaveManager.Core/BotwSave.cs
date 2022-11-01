#pragma warning disable IDE0063 // Use simple 'using' statement

using BotwSaveManager.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwSaveManager.Core
{
    public enum SaveType : byte {
        WiiU = 0x00,
        Switch = 0x1B
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

        private static readonly string[] Hashes = new string[] {
            "7B74E117", "17E1747B", "D913B769", "69B713D9", "B666D246", "46D266B6", "021A6FF2",
            "F26F1A02", "FF74960F", "0F9674FF", "8932285F", "5F283289", "3B0A289B", "9B280A3B",
            "2F95768F", "8F76952F", "9C6CFD3F", "3FFD6C9C", "BBAC416B", "6B41ACBB", "CCAB71FD",
            "FD71ABCC", "CBC6B5E4", "E4B5C6CB", "2CADB0E7", "E7B0AD2C", "A6EB3EF4", "F43EEBA6",
            "21D4CFFA", "FACFD421", "22A510D1", "D110A522", "98D10D53", "530DD198", "55A22047",
            "4720A255", "E5A63A33", "333AA6E5", "BEC65061", "6150C6BE", "BC118370", "708311BC",
            "0E9D0E75", "750E9D0E"
        };

        public SaveType SaveType { get; set; }
        public string SourceFolder { get; set; }
        public List<string> VersionList { get; set; } = new();

        public BotwSave(string sourceFolder, bool skipVersionCheck = false)
        {
            SourceFolder = sourceFolder;

            if (!File.Exists($"{sourceFolder}/options.sav")) {
                throw new FileNotFoundException(
                    $"Invalid BotwSave folder: '{sourceFolder}'\n" +
                    $"The selected folder does not contain an 'options.sav' file."
                );
            }

            // Read save type
            using (FileStream fs = File.OpenRead($"{sourceFolder}/options.sav")) {
                SaveType = (SaveType)fs.ReadByte();
            }

            foreach (var file in Directory.EnumerateFiles(SourceFolder, "game_date.sav", SearchOption.AllDirectories)) {

                try {
                    using (FileStream fs = File.OpenRead($"{sourceFolder}/options.sav")) {

                        // Read header data
                        byte[] headerData = new byte[2];
                        fs.Read(headerData, SaveType == SaveType.WiiU ? 1 : 0, 2);

                        // Reverse header on WiiU files
                        if (SaveType == SaveType.WiiU) {
                            Array.Reverse(headerData);
                        }

                        // Collect version
                        VersionList.Add(Versions[Headers.AsSpan().IndexOf(BitConverter.ToUInt16(headerData))]);
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
    }
}
