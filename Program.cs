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

        private static void Main(string[] args)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "option.sav"))
            {
                Console.WriteLine("BOTW_SaveConv");
                Console.WriteLine("");
                Console.WriteLine("This tool is still in beta and might not be perfect yet, so please make a backup of your save before using it.");
                Console.WriteLine("");
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
                        Console.WriteLine("\n" + From + " version of BOTW detected, starting conversion to the " + To + " version...\n");
                    }

                    using (var progress = new ProgressBar())
                    {
                        Console.WriteLine("\nProcessing " + Path.GetFileName(Path.GetDirectoryName(file)) + "/" + Path.GetFileName(file));
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
                Console.WriteLine("\nConversion " + From + "->" + To + " done!");
            }
            else
            {
                Console.WriteLine("BOTW_SaveConv");
                Console.WriteLine("by WemI0");
                Console.WriteLine("");
                Console.WriteLine("Convert WiiU <-> Switch BOTW Savegame");
                Console.WriteLine("");
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

            List<string> list = new List<string>();
            list.Add("Item");
            list.Add("Weap");
            list.Add("Armo");
            list.Add("Fire");
            list.Add("Norm");
            list.Add("IceA");
            list.Add("Elec");
            list.Add("Bomb");
            list.Add("Anci");
            list.Add("Anim");
            list.Add("Obj_");
            list.Add("Game");
            list.Add("Dm_N");
            list.Add("Dm_A");
            list.Add("Dm_E");
            list.Add("Dm_P");
            list.Add("FldO");
            list.Add("Gano");
            list.Add("Gian");
            list.Add("Grea");
            list.Add("KeyS");
            list.Add("Kokk");
            list.Add("Liza");
            list.Add("Mann");
            list.Add("Mori");
            list.Add("Npc_");
            list.Add("OctO");
            list.Add("Octa");
            list.Add("Octa");
            list.Add("arro");
            list.Add("Pict");
            list.Add("PutR");
            list.Add("Rema");
            list.Add("Site");
            list.Add("TBox");
            list.Add("TwnO");
            list.Add("Prie");
            list.Add("Dye0");
            list.Add("Dye1");
            list.Add("Map");
            list.Add("Play");
            list.Add("Oasi");
            list.Add("Cele");
            list.Add("Wolf");
            list.Add("Gata");
            list.Add("Ston");
            list.Add("Kaka");
            list.Add("Soji");
            list.Add("Hyru");
            list.Add("Powe");
            list.Add("Lana");
            list.Add("Hate");
            list.Add("Akka");
            list.Add("Yash");
            list.Add("Dung");
            list.Add("BeeH");
            list.Add("Boar");
            list.Add("Boko");
            list.Add("Brig");
            list.Add("DgnO");

            bool a = list.Any(Object.Contains);
            return a;
        }
    }
}