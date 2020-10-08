using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BotWSaveManager.Conversion
{
    public class Save
    {
        public enum SaveType
        {
            WiiU,
            Switch
        }

        public SaveType SaveConsoleType;
        public string SaveFolder;

        public List<string> SaveVersionList = new List<string>();

        private bool skip;

        private static List<int> filesizes = new List<int>
        {
            896976,
            897160,
            897112,
            907824,
            1020648,
            1027208,
            1027208
        };

        private static List<int> headers = new List<int>
        {
            0x24e2,
            0x24EE,
            0x2588,
            0x29c0,
            0x3ef8,
            0x471a,
            0x471b,
            0x471e
        };

        private static List<string> versionList = new List<string>
        {
            "v1.0",
            "v1.1",
            "v1.2",
            "v1.3",
            "v1.3.3",
            "v1.4",
            "v1.5",
            "v1.6"
        };

        private static List<string> items = new List<string>
        {
            "Item", "Weap", "Armo", "Fire", "Norm", "IceA", "Elec", "Bomb", "Anci", "Anim",
            "Obj_", "Game", "Dm_N", "Dm_A", "Dm_E", "Dm_P", "FldO", "Gano", "Gian", "Grea",
            "KeyS", "Kokk", "Liza", "Mann", "Mori", "Npc_", "OctO", "Octa", "Octa", "arro",
            "Pict", "PutR", "Rema", "Site", "TBox", "TwnO", "Prie", "Dye0", "Dye1", "Map",
            "Play", "Oasi", "Cele", "Wolf", "Gata", "Ston", "Kaka", "Soji", "Hyru", "Powe",
            "Lana", "Hate", "Akka", "Yash", "Dung", "BeeH", "Boar", "Boko", "Brig", "DgnO"
        };

        private static List<string> hash = new List<string>
        {
            "7B74E117", "17E1747B", "D913B769", "69B713D9", "B666D246", "46D266B6", "021A6FF2",
            "F26F1A02", "FF74960F", "0F9674FF", "8932285F", "5F283289", "3B0A289B", "9B280A3B",
            "2F95768F", "8F76952F", "9C6CFD3F", "3FFD6C9C", "BBAC416B", "6B41ACBB", "CCAB71FD",
            "FD71ABCC", "CBC6B5E4", "E4B5C6CB", "2CADB0E7", "E7B0AD2C", "A6EB3EF4", "F43EEBA6",
            "21D4CFFA", "FACFD421", "22A510D1", "D110A522", "98D10D53", "530DD198", "55A22047",
            "4720A255", "E5A63A33", "333AA6E5", "BEC65061", "6150C6BE", "BC118370", "708311BC",
            "0E9D0E75", "750E9D0E"
        };

        public Save(string folder, bool skipSwitchVersionCheck = false)
        {
            if (!File.Exists(Path.Combine(folder, "option.sav")))
            {
                throw new ArgumentException("The selected folder is not a valid Breath of the Wild save folder. " +
                                            "Please select a folder containing valid save data, including option.sav " +
                                            "in the root of the folder.");
            }

            using (FileStream fs = new FileStream(Path.Combine(folder, "option.sav"), FileMode.Open))
            using (BinaryReader br = new BinaryReader(fs))
            {
                byte[] check = br.ReadBytes(1);
                this.SaveConsoleType = ByteArrayToString(check) == "00" ? SaveType.WiiU : SaveType.Switch;
            }

            foreach (string file in Directory.EnumerateFiles(folder, "game_data.sav", SearchOption.AllDirectories))
            {
                using (FileStream fs = new FileStream(file, FileMode.Open))
                using (BinaryReader br = new BinaryReader(fs))
                {
                    // Not a reliable way to determine game version
                    //this.GameVersion = versionList[filesizes.IndexOf((int)f.Length)];

                    if (this.SaveConsoleType == SaveType.WiiU)
                    {
                        while (ByteArrayToString(br.ReadBytes(1)) == "00")
                        {
                        }

                        br.BaseStream.Position -= 1;

                        byte[] backwardsHeader = br.ReadBytes(2);

                        try
                        {
                            this.SaveVersionList.Add(versionList[headers.IndexOf(BitConverter.ToInt16(backwardsHeader.Reverse().ToArray(), 0))]);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw new UnsupportedSaveException("The version of a numbered save folder you selected cannot be retrieved.");
                        }
                    }
                    else
                    {
                        try
                        {
                            if (BitConverter.ToInt16(br.ReadBytes(2), 0) == 0x2a46)
                            {
                                this.SaveVersionList.Add(versionList[headers.IndexOf(0x29c0)]); //v1.3.0 switch?
                                return;
                            }

                            br.BaseStream.Position = 0;

                            if (BitConverter.ToInt16(br.ReadBytes(2), 0) == 0x3ef9) 
                            {
                                this.SaveVersionList.Add(versionList[headers.IndexOf(0x3ef8)]); //v1.3.3 switch?
                                return;
                            }

                            br.BaseStream.Position = 0;

                            this.SaveVersionList.Add(versionList[headers.IndexOf(BitConverter.ToInt16(br.ReadBytes(2), 0))]);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            if (!skipSwitchVersionCheck)
                            {
                                throw new UnsupportedSaveException("The version of a numbered save folder you selected cannot be retrieved.") {IsSwitch = true};
                            }
                        }
                    }
                }
            }

            this.SaveFolder = folder;
        }

        public Dictionary<string, byte[]> ConvertSave(string outputLocation = null)
        {
            Dictionary<string, byte[]> returnBytesDict = new Dictionary<string, byte[]>();

            foreach (string file in Directory.GetFiles(this.SaveFolder, "*.sav", SearchOption.AllDirectories))
            {
                byte[] currentFileBytes = File.ReadAllBytes(file);

                using (MemoryStream ms = new MemoryStream(currentFileBytes))
                using (BinaryReader br = new BinaryReader(ms))
                {
                    for (int h = 0; h < ms.Length / 4; h++)
                    {
                        if (file.Contains("trackblock") && h == 0)
                        {
                            br.BaseStream.Position = 4;
                            byte[] reverseHeader = br.ReadBytes(2);

                            Array.Reverse(reverseHeader);

                            BinaryWriter endianUpd = new BinaryWriter(ms);
                            ms.Position = 4;
                            endianUpd.Write(reverseHeader);
                            h = 2;
                        }

                        br.BaseStream.Position = h * 4;
                        byte[] endianConv = br.ReadBytes(4);

                        if (hash.Contains(ByteArrayToString(endianConv))) // skip strings
                        {
                            Array.Reverse(endianConv);

                            BinaryWriter endianUpd = new BinaryWriter(ms);
                            ms.Position = h * 4;
                            endianUpd.Write(endianConv);
                            h++;
                            this.skip = true;
                        }
                        else
                        {
                            this.skip = false;
                        }

                        if (CheckString(endianConv) == false && this.skip == false) // make sure we don't convert strings
                        {
                            Array.Reverse(endianConv);

                            BinaryWriter endianUpd = new BinaryWriter(ms);
                            ms.Position = h * 4;
                            endianUpd.Write(endianConv);
                        }
                        else if (this.skip == false)
                        {
                            h++;
                            for (int i = 0; i < 16; i++)
                            {
                                br.BaseStream.Position = (h + (i * 2)) * 4;
                                byte[] endianHash = br.ReadBytes(4);

                                Array.Reverse(endianHash);

                                BinaryWriter endianUpd = new BinaryWriter(ms);
                                ms.Position = (h + (i * 2)) * 4;
                                endianUpd.Write(endianHash);
                            }

                            h = h + 0x1E;
                        }
                    }

                    if (outputLocation != null)
                    {
                        File.WriteAllBytes(outputLocation, ms.ToArray());
                    }

                    returnBytesDict.Add(file, ms.ToArray());
                }
            }
            
            this.SaveConsoleType = this.SaveConsoleType == SaveType.Switch ? SaveType.WiiU : SaveType.Switch;

            return returnBytesDict;
        }

        public static string ByteArrayToString(byte[] byteA)
        {
            string hex = BitConverter.ToString(byteA);
            return hex.Replace("-", "");
        }

        public static bool CheckString(byte[] toCheck)
        {
            string Object = System.Text.Encoding.UTF8.GetString(toCheck);
            return items.Any(Object.Contains);
        }
    }
}
