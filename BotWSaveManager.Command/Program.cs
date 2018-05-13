using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BotWSaveManager.Conversion;
using BotWSaveManager.Conversion.IO;

namespace BotWSaveManager.Command
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Breath of the Wild Save Manager by Jordan Zeotni, fork of https://github.com/WemI0/BOTW_SaveConv");
            Console.Write("Enter the path of the option.sav file in your save folder or press enter if the file is in the same folder as this application (option.sav): ");
            string fileLocation = Console.ReadLine()?.Trim('"');

            if (string.IsNullOrWhiteSpace(fileLocation))
            {
                if (Directory.GetFiles(Globals.AppPath, "*.sav").First() == null)
                {
                    Console.WriteLine("There are no files with the extension *.sav in this folder. Either place this applicaton in the same folder as option.sav or enter the save file's path.");
                    Main(args);
                    return;
                }

                fileLocation = Path.Combine(Globals.AppPath, Directory.GetFiles(Globals.AppPath, "*.sav").First());
            }
            else if (!File.Exists(fileLocation))
            {
                Console.WriteLine("This file does not exist at this path. Either place one in the root directory or enter the path of option.sav.");
                Main(args);
                return;
            }

            Save selectedSave;

            try
            {
                selectedSave = new Save(fileLocation);
            }
            catch (UnsupportedSaveException e)
            {
                if (e.IsSwitch)
                {
                    Console.Write("Cannot find switch version from save. If you would like to attempt to use this file anyways, enter \"Y\": ");

                    if (Console.ReadKey().Key == ConsoleKey.Y)
                    {
                        selectedSave = new Save(fileLocation, true);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("This save is not supported. If you think this is an error report the following stacktrace:");
                        Console.WriteLine(e);
                        Console.WriteLine();
                        Console.WriteLine();
                        Main(args);
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("This save is not supported. If you think this is an error report the following stacktrace:");
                    Console.WriteLine(e);
                    Console.WriteLine();
                    Console.WriteLine();
                    Main(args);
                    return;
                }
            }

            if (selectedSave.SaveConsoleType == Save.SaveType.Switch && selectedSave.GameVersion != "Unknown version")
            {
                Console.WriteLine("Please note that Switch versioning is WIP until more saves from varying game versions is available.");
                Console.WriteLine();
            }

            Console.WriteLine(selectedSave.SaveConsoleType == Save.SaveType.Switch ? $"BotW {selectedSave.GameVersion} - Switch" : $"BotW {selectedSave.GameVersion} - Wii U");
            Console.WriteLine("Press any key to begin conversion, or ESC to cancel...");
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);

            if (consoleKeyInfo.Key == ConsoleKey.Escape)
            {
                return;
            }

            Dictionary<string, byte[]> convertSaveBytes = selectedSave.ConvertSave();

            Console.Write("Enter a folder to write the save to, or press enter to overwrite the existing save with the converted version: ");
            string saveLocation = Console.ReadLine()?.Trim('"');

            try
            {
                if (string.IsNullOrWhiteSpace(saveLocation))
                {
                    foreach (KeyValuePair<string, byte[]> convertSaveByte in convertSaveBytes)
                    {
                        File.WriteAllBytes(convertSaveByte.Key, convertSaveByte.Value);
                    }
                }
                else
                {
                    CopyDir.Copy(selectedSave.SaveFolder, saveLocation);

                    foreach (KeyValuePair<string, byte[]> convertSaveByte in convertSaveBytes)
                    {
                        string saveTo = Directory.GetFiles(saveLocation, "*.sav", SearchOption.AllDirectories)
                            .First(e => Path.GetFileName(e) == "option.sav" ||
                                Path.GetFileName(e) == Path.GetFileName(convertSaveByte.Key) && 
                                Directory.GetParent(e).Name == Directory.GetParent(convertSaveByte.Key).Name);

                        File.WriteAllBytes(saveTo, convertSaveByte.Value);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The files cannot be saved at this path. If you think this is an error report the following stacktrace:");
                Console.WriteLine(e);
                Console.WriteLine();
                Console.WriteLine();
                Main(args);
                return;
            }

            Console.WriteLine($"Files saved successfully! Please note that you are required to use {selectedSave.GameVersion} of BotW on the target system with (probably) the same DLC for this save file to work. \nPress any key to close...");
            Console.ReadKey(true);
        }
    }
}
