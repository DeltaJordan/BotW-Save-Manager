using Avalonia.MenuFactory.Attributes;
using AvaloniaGenerics.Dialogs;
using BotwSaveManager.Views;
using Material.Icons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwSaveManager.Models
{
    public class MenuModel
    {
        [Menu("Open Save", "_File", "Ctrl + O", Icon = MaterialIconKind.FolderOpenOutline)]
        public async void OpenSave()
        {
            string? folder = await BrowserDialog.Folder.ShowDialog("Open Botw Save Folder");

            if (folder != null) {

            }
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
