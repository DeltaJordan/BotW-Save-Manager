using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using AvaloniaGenerics.Dialogs;
using BotwSaveManager.Core;
using BotwSaveManager.Core.Helpers;
using Material.Icons;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwSaveManager.ViewModels
{
    public class BotwSaveViewModel : ReactiveObject
    {
        public BotwSave BotwSave { get; set; }

        private string image = null!;
        public string Image {
            get => image;
            set => this.RaiseAndSetIfChanged(ref image, value);
        }

        private string accentBrush = null!;
        public string AccentBrush {
            get => accentBrush;
            set => this.RaiseAndSetIfChanged(ref accentBrush, value);
        }

        private Thickness rezisePadding = new(0);
        public Thickness RezisePadding {
            get => rezisePadding;
            set => this.RaiseAndSetIfChanged(ref rezisePadding, value);
        }

        public async void Convert()
        {
            try {
                string? output = await BrowserDialog.Folder.ShowDialog("Open Botw Save Folder");
                if (output != null) {
                    await Task.Run(() => BotwSave.ConvertPlatform(output));
                    await MessageBox.Show($"Save succefully converted!", "Notice", icon: MaterialIconKind.InfoCircleOutline);
                }

                ViewModel.Content = null;
            }
            catch (Exception ex) {
                Logger.Write(ex);
                await MessageBox.Show(ex.Message, "Error", icon: MaterialIconKind.FileDocumentErrorOutline);
            }
        }

        public void Cancel() => ViewModel.Content = null;

        public BotwSaveViewModel(BotwSave save)
        {
            BotwSave = save;
            Image = $"/Assets/{save.SaveType}.png";
            AccentBrush = save.SaveType == SaveType.Switch ? "#CB4041" : "#00ACCA";
            RezisePadding = BotwSave.VersionList.Count > 8 ? new(0, 0, 0, 4) : new(0);
        }
    }
}
