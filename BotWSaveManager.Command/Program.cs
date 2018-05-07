using System;
using System.IO;
using System.Linq;
using BotWSaveManager.Conversion;

namespace BotWSaveManager.Command
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Breath of the Wild Save Manager by Jordan Zeotni, fork of https://github.com/WemI0/BOTW_SaveConv");
            Console.Write("Enter the path of the save file or press enter if the file is in the same folder as this application (option.sav): ");
            string fileLocation = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(fileLocation))
            {
                if (Directory.GetFiles(Globals.AppPath, "*.sav").First() == null)
                {
                    Console.WriteLine("There are no files with the extension *.sav in this folder. Either place one in the root directory or enter the save file's path.");
                    Main(args);
                    return;
                }

                fileLocation = Path.Combine(Globals.AppPath, Directory.GetFiles(Globals.AppPath, "*.sav").First());
            }
            else if (!File.Exists(fileLocation))
            {
                Console.WriteLine("This file does not exist at this path. Either place one in the root directory or enter the save file's path.");
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
                Console.WriteLine("This save is not supported. If you think this is an error report the following stacktrace:");
                Console.WriteLine(e);
                Console.WriteLine();
                Console.WriteLine();
                Main(args);
                return;
            }

            Console.WriteLine(selectedSave.SaveConsoleType == Save.SaveType.Switch ? $"BotW {selectedSave.GameVersion} - Switch" : $"BotW {selectedSave.GameVersion} - Wii U");
            Console.WriteLine("Press any key to begin conversion, or ESC to cancel...");
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);

            if (consoleKeyInfo.Key == ConsoleKey.Escape)
            {
                return;
            }

            byte[] convertSaveBytes = selectedSave.ConvertSave();

            Console.Write("Enter a path to save the file as, or press enter to save as \"option.sav\" in the application folder: ");
            string saveLocation = Console.ReadLine();

            try
            {
                File.WriteAllBytes(string.IsNullOrWhiteSpace(saveLocation) ? Path.Combine(Globals.AppPath, "option.sav") : saveLocation, convertSaveBytes);
            }
            catch (Exception e)
            {
                Console.WriteLine("The file cannot be saved at this path. If you think this is an error report the following stacktrace:");
                Console.WriteLine(e);
                Console.WriteLine();
                Console.WriteLine();
                Main(args);
                return;
            }

            Console.WriteLine($"File saved successfully! Please note that you are required to use {selectedSave.GameVersion} of BotW on the target system with (probably) the same DLC for this save file to work. This application only supports conversion at the moment, stay tuned for more features.\nPress any key to close...");
            Console.ReadKey(true);
        }
    }
}
