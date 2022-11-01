using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.MenuFactory;
using AvaloniaGenerics.Dialogs;
using BotwSaveManager.Core;
using BotwSaveManager.Core.Helpers;
using BotwSaveManager.Models;
using Material.Icons;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace BotwSaveManager.Views
{
    public partial class AppView : Window
    {
        public MenuModel MenuModel = new();
        public AppView()
        {
            AvaloniaXamlLoader.Load(this);
#if DEBUG
            this.AttachDevTools();
#endif

            this.FindControl<Menu>("RootMenu")!.Items = MenuFactory.Generate(MenuModel);

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

        public async void DragDropEvent(object? sender, DragEventArgs e)
        {
            string? folder = e.Data.GetFileNames()?.FirstOrDefault();

            if (folder != null && Directory.Exists(folder)) {
                try {
                    BotwSave save = new(folder, true);
                    ViewModel.Content = new BotwSaveView(save);
                }
                catch (Exception ex) {
                    Logger.Write(ex);
                    await MessageBox.Show(ex.Message, "Error", icon: MaterialIconKind.FileDocumentErrorOutline);
                }
            }
        }
    }
}
