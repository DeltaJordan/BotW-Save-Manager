using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using AvaloniaGenerics.Dialogs;
using BotwSaveManager.Core.Helpers;
using Material.Icons;
using System;

namespace BotwSaveManager
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            try {
                BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
            }
            catch (Exception ex) {
                try {
                    Logger.Write(ex);
                }
                finally {
                    MessageBox.ShowSync(ex.ToString(), "Unhandled Exception", icon: MaterialIconKind.ErrorOutline);
                }
            }
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
    }
}
