
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using BotwSaveManager.Core;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwSaveManager.ViewModels
{
    public class AppViewModel : ReactiveObject
    {
        private static readonly TextBlock Default = new() {
            Text = "Drag and drop a save folder to open it.",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        private object content = Default;
        public object Content {
            get => content ?? Default;
            set => this.RaiseAndSetIfChanged(ref content, value);
        }

        //
        // Window Settings

        public void ChangeState() => View.WindowState = View.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        public void Minimize() => View.WindowState = WindowState.Minimized;
        public void Quit() => View.MenuModel.Quit();

        private bool isMaximized = false;
        public bool IsMaximized {
            get => isMaximized;
            set => this.RaiseAndSetIfChanged(ref isMaximized, value);
        }
    }
}
