#pragma warning disable CA1822 // Mark members as static

using Avalonia.Controls;
using Avalonia.Input;
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
