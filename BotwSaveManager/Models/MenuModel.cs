using Avalonia.MenuFactory.Attributes;
using AvaloniaGenerics.Dialogs;
using BotwSaveManager.Core;
using BotwSaveManager.Core.Helpers;
using BotwSaveManager.ViewModels;
using BotwSaveManager.Views;
using Material.Icons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
                    BotwSave save = new(folder, true);
                    ViewModel.Content = new BotwSaveView(save);
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
                            App.LogsView.Load();
                            await Task.Run(() => save.ConvertPlatform(output));
                            await MessageBox.Show($"Save succefully converted!", "Notice", icon: MaterialIconKind.InfoCircleOutline);
                            App.LogsView.Unload();
                        }
                    }
                }
                catch (Exception ex) {
                    Logger.Write(ex);
                    await MessageBox.Show(ex.Message, "Error", icon: MaterialIconKind.FileDocumentErrorOutline);
                }
            }
        }

        [Menu("Open Debug Log", "_File", Icon = MaterialIconKind.FileDocumentErrorOutline, IsSeparator = true)]
        public void OpenDebugLog()
        {
            App.LogsView.Load();
        }

        [Menu("Clear Debug Log", "_File", Icon = MaterialIconKind.FileDocumentRemoveOutline)]
        public void ClearDebugLog()
        {
            (App.LogsView.DataContext as LogsViewModel)?.LogTrace.Clear();
        }

        [Menu("Clear Logs Folder", "_File", Icon = MaterialIconKind.FolderCancelOutline, IsSeparator = true)]
        public async void ClearLogsFolder()
        {
            Logger.Write("Clearing logs folder...");

            int i = 0;
            foreach (var file in Directory.EnumerateFiles($"./Logs", "*.log", SearchOption.TopDirectoryOnly)) {
                if (!file.ToCommonPath().EndsWith(Logger.CurrentLog!)) {
                    File.Delete(file);
                    i++;
                }
            }

            string prompt = $"Removed {i} log(s) from \"./Logs\"";
            await MessageBox.Show(prompt, "Notice", icon: MaterialIconKind.InfoCircleOutline);
            Logger.Write(prompt);
        }

        [Menu("Quit", "_File", "Alt + F4", Icon = MaterialIconKind.ExitToApp, IsSeparator = true)]
        public async void Quit()
        {
            Environment.Exit(0);
        }

        [Menu("Help", "_About", "F1", Icon = MaterialIconKind.Help)]
        public void Help()
        {
            HelpView help = new();
            help.ShowDialog(View);
        }

        [Menu("Readme", "_About", "F2", Icon = MaterialIconKind.HandshakeOutline)]
        public async void Readme()
        {
            await MessageBox.Show(Resource.Load("README.md").ToString(), "Readme", formatting: Formatting.Markdown, icon: MaterialIconKind.HandshakeOutline);
        }
    }
}
