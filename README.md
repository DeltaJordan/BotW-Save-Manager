# Breath of the Wild Save Manager

*This tool is useless if all you want to do is edit your save, the following link can directly edit Switch saves without any conversion: https://www.marcrobledo.com/savegame-editors/zelda-botw/*

**Note: Make sure both versions of BotW have the same update version**

BotW Save Manager is a fork of https://github.com/WemI0/BOTW_SaveConv that at the moment can convert Switch <-> Wii U BotW save files. It currently has been written with .Net Framework for Windows with UI, and .Net Core (command-line only) for cross-compatibility.

## Usage:

### Windows-only UI

1. Select File>Open save>option.sav in your save folder.
2. Click convert and wait for it to finish.
3. Click Browse to the folder you want to save it to.
4. Click Save and the application should write it to the folder.

### Cross-platform command-line

1. Enter the path of option.sav when prompted.
2. Wait for the files to convert.
3. Either select a folder for it to save to or press Enter to overwrite your save with the converted version.
4. Once it finishes you'll be good to go to copy it to your console.

## Dependencies:

* Windows
  * [.Net Framework 4.7](https://www.microsoft.com/en-us/download/details.aspx?id=55170)
* Other
  * .Net Core - varies by system
