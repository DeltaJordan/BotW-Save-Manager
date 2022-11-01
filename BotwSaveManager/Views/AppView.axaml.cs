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
#if DEBUG
            this.AttachDevTools();
#endif

            this.FindControl<Menu>("RootMenu")!.Items = MenuFactory.Generate(new MenuModel());

            DropTarget = this.FindControl<Grid>("DropTarget");
            DropTarget!.AddHandler(DragDrop.DropEvent, DragDropEvent);
        }

        protected override void HandleWindowStateChanged(WindowState state)
        {
            if (state == WindowState.Maximized) {
                Padding = new Thickness(7);
                ExtendClientAreaTitleBarHeightHint = 44;
                ViewModel.IsMaximized = true;
            }
            else {
                Padding = new Thickness(0);
                ExtendClientAreaTitleBarHeightHint = 30;
                ViewModel.IsMaximized = false;
            }

            base.HandleWindowStateChanged(state);
        }

        public void DragDropEvent(object? sender, DragEventArgs e)
        {
            Debug.WriteLine(e.Data.GetFileNames()?.FirstOrDefault());
        }
    }
}
