using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BOTW_SaveConv
{
    internal class Program
    {
        public enum ConversionDirection
        {
            WiiUToSwitch,
            SwitchToWiiU
        }

        public static ConversionDirection Direction;
        public static bool Skip;

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

        private static void Main(string[] args) // hacky code, sorry if you read this but it does the job, feel free to improve it :)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "option.sav"))
            {
                Console.WriteLine("BOTW_SaveConv" + Environment.NewLine);
                Console.WriteLine("Make sure you made a backup of your save files in case the conversion fail." + Environment.NewLine);
                Console.WriteLine("Press Y to continue, press any other key to abort.");
                if (Console.ReadKey().Key != ConsoleKey.Y)
                    Environment.Exit(0);
                else
                    ClearLine();

                foreach (string file in Directory.EnumerateFiles(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "*.sav", SearchOption.AllDirectories))
                {
                    FileStream fs = new FileStream(file, FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    FileInfo f = new FileInfo(file);

                    if (Path.GetFileName(file) == "option.sav")
                    {
                        br.BaseStream.Position = 0;
                        byte[] check = br.ReadBytes(Convert.ToInt32(1));
                        Direction = ByteArrayToString(check) == "00" ? ConversionDirection.WiiUToSwitch : ConversionDirection.SwitchToWiiU;
                        Console.WriteLine(Environment.NewLine + From + " version of BOTW detected, starting conversion to the " + To + " version..." + Environment.NewLine);
                    }

                    using (var progress = new ProgressBar())
                    {
                        Console.WriteLine(Environment.NewLine + "Processing " + Path.GetFileName(Path.GetDirectoryName(file)) + "/" + Path.GetFileName(file));
                        for (int h = 0; h < f.Length / 4; h++)
                        {
                            br.BaseStream.Position = h * 4;
                            byte[] EndianConv = br.ReadBytes(Convert.ToInt32(4));

                            if (Hash.Contains(ByteArrayToString(EndianConv))) // skip strings
                            {
                                Array.Reverse(EndianConv);

                                BinaryWriter EndianUpd = new BinaryWriter(fs);
                                fs.Position = h * 4;
                                EndianUpd.Write(EndianConv);
                                h++;
                                Skip = true;
                            }
                            else
                            {
                                Skip = false;
                            }

                            if (CheckString(EndianConv) == false && Skip == false) // make sure we don't convert strings
                            {
                                Array.Reverse(EndianConv);

                                BinaryWriter EndianUpd = new BinaryWriter(fs);
                                fs.Position = h * 4;
                                EndianUpd.Write(EndianConv);
                            }
                            else if (Skip == false)
                            {
                                h++;
                                for (int i = 0; i < 16; i++)
                                {
                                    br.BaseStream.Position = (h + (i * 2)) * 4;
                                    byte[] EndianHash = br.ReadBytes(Convert.ToInt32(4));

                                    Array.Reverse(EndianHash);

                                    BinaryWriter EndianUpd = new BinaryWriter(fs);
                                    fs.Position = (h + (i * 2)) * 4;
                                    EndianUpd.Write(EndianHash);
                                }
                                h = h + 0x1E;
                            }

                            progress.Report((double)h * 4 / f.Length);
                        }
                        ClearLine();
                        ClearLine();
                        Console.WriteLine(Path.GetFileName(Path.GetDirectoryName(file)) + "/" + Path.GetFileName(file) + " processed.");
                    }
                    br.Close();
                }
                Console.WriteLine(Environment.NewLine + "Conversion " + From + "->" + To + " done!");
            }
            else
            {
                Console.WriteLine("BOTW_SaveConv");
                Console.WriteLine("by WemI0" + Environment.NewLine);
                Console.WriteLine("Converts WiiU <-> Switch BOTW Savegame" + Environment.NewLine);
                Console.WriteLine("Usage: Put 'BOTW_SaveConv.exe' into the folder that contains option.sav, and run it");
                Console.ReadLine();
                Environment.Exit(0);
            }
            Console.ReadLine();
        }

        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
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