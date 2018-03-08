using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BOTW_SaveConv
{
    internal class Program
    {
        public static string From;
        public static string To;
        private static List<string> Items = new List<string>
        {"Item", "Weap", "Armo", "Fire", "Norm", "IceA", "Elec", "Bomb", "Anci", "Anim",
        "Obj_", "Game", "Dm_N", "Dm_A", "Dm_E", "Dm_P", "FldO", "Gano", "Gian", "Grea",
        "KeyS", "Kokk", "Liza", "Mann", "Mori", "Npc_", "OctO", "Octa", "Octa", "arro",
        "Pict", "PutR", "Rema", "Site", "TBox", "TwnO", "Prie", "Dye0", "Dye1", "Map",
        "Play", "Oasi", "Cele", "Wolf", "Gata", "Ston", "Kaka", "Soji", "Hyru", "Powe",
        "Lana", "Hate", "Akka", "Yash", "Dung", "BeeH", "Boar", "Boko", "Brig", "DgnO" };

        private static void Main(string[] args)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "option.sav"))
            {
                Console.WriteLine("BOTW_SaveConv" + Environment.NewLine);
                Console.WriteLine("This tool is still in beta and might not be perfect yet, so please make a backup of your save before using it." + Environment.NewLine);
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
                        if (ByteArrayToString(check) == "00")
                        {
                            From = "WiiU";
                            To = "Switch";
                        }
                        else
                        {
                            From = "Switch";
                            To = "WiiU";
                        }
                        Console.WriteLine(Environment.NewLine + From + " version of BOTW detected, starting conversion to the " + To + " version..." + Environment.NewLine);
                    }

                    using (var progress = new ProgressBar())
                    {
                        Console.WriteLine(Environment.NewLine + "Processing " + Path.GetFileName(Path.GetDirectoryName(file)) + "/" + Path.GetFileName(file));
                        for (int h = 0; h < f.Length / 4; h++)
                        {
                            br.BaseStream.Position = h * 4;
                            byte[] EndianConv = br.ReadBytes(Convert.ToInt32(4));

                            if (ByteArrayToString(EndianConv) == "7B74E117" || ByteArrayToString(EndianConv) == "17E1747B") // skip horse name
                            {
                                h = h + 0xC0;
                            }

                            if (CheckString(EndianConv) == false) // skip item string
                            {
                                Array.Reverse(EndianConv);

                                BinaryWriter EndianUpd = new BinaryWriter(fs);
                                fs.Position = h * 4;
                                EndianUpd.Write(EndianConv);
                            }
                            else
                            {
                                h = h + 0x1F;
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
