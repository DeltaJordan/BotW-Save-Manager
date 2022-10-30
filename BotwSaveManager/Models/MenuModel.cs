using Avalonia.MenuFactory.Attributes;
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
        public void OpenSave()
        {

        }

        [Menu("Readme", "_About", Icon = MaterialIconKind.HandshakeOutline)]
        public void Readme()
        {

        }

        [Menu("Help", "_About", Icon = MaterialIconKind.Help)]
        public void Help()
        {

        }
    }
}
