
// 
// Credits
using BotwSaveManager.Core;
using BotwSaveManager.Core.Helpers;
using System.Diagnostics;

Console.WriteLine("Breath of the Wild Save Manager by Jordan Marine, fork of https://github.com/WemI0/BOTW_SaveConv, rewritten and updated by Arch Leaders\n\n");
Console.Write("Drag and drop your save folder here and press enter: ");
string? input = Console.ReadLine();

Logger.Initialize(new ConsoleTraceListener());

if (input != null && Directory.Exists(input)) {
    BotwSave save = new(input, true);

    Console.Write($"\nIdentified {save.SaveType} save, convert it to {save.SaveType.Reverse()}? (Y/n) ");
    string? responce = Console.ReadLine();

    if ((responce ?? "").ToLower() != "n") {
        Console.Write("\nDrag and drop your save folder output here and press enter: ");
        string? output = Console.ReadLine();


        if (output != null) {
            try {
                Directory.CreateDirectory(output);
                save.ConvertPlatform(output);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nSave converted succcefully to {save.SaveType}: '{output}'");
            }
            catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Logger.Write(ex);
            }

            Console.ResetColor();
        }
    }
}
else {
    Console.WriteLine("Please enter a valid folder parh.");
}

Console.WriteLine("\nPress enter to exit. . .");
Console.ReadLine();