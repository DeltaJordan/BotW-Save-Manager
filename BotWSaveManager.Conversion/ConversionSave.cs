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
        public string FileLocation;

        private bool skip;

        private static List<string> Items = new List<string>
        {
            "Item", "Weap", "Armo", "Fire", "Norm", "IceA", "Elec", "Bomb", "Anci", "Anim",
            "Obj_", "Game", "Dm_N", "Dm_A", "Dm_E", "Dm_P", "FldO", "Gano", "Gian", "Grea",
            "KeyS", "Kokk", "Liza", "Mann", "Mori", "Npc_", "OctO", "Octa", "Octa", "arro",
            "Pict", "PutR", "Rema", "Site", "TBox", "TwnO", "Prie", "Dye0", "Dye1", "Map",
            "Play", "Oasi", "Cele", "Wolf", "Gata", "Ston", "Kaka", "Soji", "Hyru", "Powe",
            "Lana", "Hate", "Akka", "Yash", "Dung", "BeeH", "Boar", "Boko", "Brig", "DgnO"
        };

        private static List<string> Hash = new List<string>
        {
            "7B74E117", "17E1747B", "D913B769", "69B713D9", "B666D246", "46D266B6", "021A6FF2",
            "F26F1A02", "FF74960F", "0F9674FF", "8932285F", "5F283289", "3B0A289B", "9B280A3B",
            "2F95768F", "8F76952F", "9C6CFD3F", "3FFD6C9C", "BBAC416B", "6B41ACBB", "CCAB71FD",
            "FD71ABCC", "CBC6B5E4", "E4B5C6CB", "2CADB0E7", "E7B0AD2C", "A6EB3EF4", "F43EEBA6",
            "21D4CFFA", "FACFD421", "22A510D1", "D110A522", "98D10D53", "530DD198", "55A22047",
            "4720A255", "E5A63A33", "333AA6E5", "BEC65061", "6150C6BE", "BC118370", "708311BC",
            "0E9D0E75", "750E9D0E"
        };

        public Save(string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open))
            using (BinaryReader br = new BinaryReader(fs))
            {
                FileInfo f = new FileInfo(file);

                byte[] check = br.ReadBytes(Convert.ToInt32(1));
                this.SaveConsoleType = ByteArrayToString(check) == "00" ? SaveType.WiiU : SaveType.Switch;
            }

            this.FileLocation = file;
        }

        public byte[] ConvertSave(string outputLocation = null)
        {
            byte[] currentFileBytes = File.ReadAllBytes(this.FileLocation);

            using (MemoryStream ms = new MemoryStream(currentFileBytes))
            using (BinaryReader br = new BinaryReader(ms))
            {
                for (int h = 0; h < currentFileBytes.Length / 4; h++)
                {
                    br.BaseStream.Position = h * 4;
                    byte[] endianConv = br.ReadBytes(Convert.ToInt32(4));

                    if (Hash.Contains(ByteArrayToString(endianConv))) // skip strings
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
                            byte[] endianHash = br.ReadBytes(Convert.ToInt32(4));

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

                this.SaveConsoleType = this.SaveConsoleType == SaveType.Switch ? SaveType.WiiU : SaveType.Switch;

                return ms.ToArray();
            }
        }

        public static string ByteArrayToString(byte[] byteA)
        {
            string hex = BitConverter.ToString(byteA);
            return hex.Replace("-", "");
        }

        public static bool CheckString(byte[] toCheck)
        {
            string Object = System.Text.Encoding.UTF8.GetString(toCheck);
            return Items.Any(Object.Contains);
        }
    }
}
