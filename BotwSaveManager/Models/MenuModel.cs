#pragma warning disable CA1822 // Mark members as static

using Avalonia.MenuFactory.Attributes;
using AvaloniaGenerics.Dialogs;
using BotwSaveManager.Core;
using BotwSaveManager.Core.Helpers;
using BotwSaveManager.Views;
using Material.Icons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwSaveManager.Models
{
    public class MenuModel
    {
        [Menu("Open Save Folder", "_File", "Ctrl + O", Icon = MaterialIconKind.FolderOpenOutline)]
        public async void OpenSaveFolder()
        {
            string? folder = await BrowserDialog.Folder.ShowDialog("Open Botw Save Folder");

            if (folder != null) {
                try {
                    ViewModel.BotwSave = new(folder, true);
                }
                catch (Exception ex)  {
                    Logger.Write(ex);
                    await MessageBox.Show(ex.Message, "Error", icon: MaterialIconKind.FileDocumentErrorOutline);
                }
            }
        }

        [Menu("Convert Save", "_File", "F3", Icon = MaterialIconKind.ContentSaveMoveOutline)]
        public async void ConvertSave()
        {
            string? folder = await BrowserDialog.Folder.ShowDialog("Open Botw Save Folder");

            if (folder != null) {
                try {
                    BotwSave save = new(folder, true);
                    if (await MessageBox.Show($"Identified {save.SaveType} save, convert to {save.SaveType.Reverse()}?", "Notice", MessageBoxButtons.YesNoCancel, icon: MaterialIconKind.ContentSaveMoveOutline) == MessageBoxResult.Yes) {
                        string? output = await BrowserDialog.Folder.ShowDialog("Open Botw Save Folder");
                        if (output != null) {
                            await Task.Run(() => save.ConvertPlatform(output));
                            await MessageBox.Show($"Save succefully converted!", "Notice", icon: MaterialIconKind.InfoCircleOutline);
                        }

                    }
                }
                catch (Exception ex) {
                    Logger.Write(ex);
                    await MessageBox.Show(ex.Message, "Error", icon: MaterialIconKind.FileDocumentErrorOutline);
                }
            }
        }

        [Menu("Quit", "_File", "Alt + F4", Icon = MaterialIconKind.ExitToApp, IsSeparator = true)]
        public async void Quit()
        {
            Environment.Exit(0);
        }

        [Menu("Readme", "_About", Icon = MaterialIconKind.HandshakeOutline)]
        public async void Readme()
        {
            await MessageBox.Show(Resource.Load("README.md").ToString(), "Readme", formatting: Formatting.Markdown, icon: MaterialIconKind.HandshakeOutline);
        }

        [Menu("Help", "_About", Icon = MaterialIconKind.Help)]
        public void Help()
        {
            HelpView help = new();
            help.ShowDialog(View);
        }
    }
}
