using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.MenuFactory;
using BotwSaveManager.Models;
using System.Diagnostics;
using System.Linq;

namespace BotwSaveManager.Views
{
    public partial class AppView : Window
    {
        public AppView()
        {
            AvaloniaXamlLoader.Load(this);

            this.FindControl<Menu>("RootMenu")!.Items = MenuFactory.Generate(new MenuModel());

            DropTarget = this.FindControl<Grid>("DropTarget");
            DropTarget!.AddHandler(DragDrop.DropEvent, DragDropEvent);
        }

        public void DragDropEvent(object? sender, DragEventArgs e)
        {
            Debug.WriteLine(e.Data.GetFileNames()?.FirstOrDefault());
        }
    }
}
