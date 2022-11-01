#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

global using static BotwSaveManager.App;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Themes.Fluent;
using AvaloniaGenerics.Dialogs;
using BotwSaveManager.Core.Helpers;
using BotwSaveManager.ViewModels;
using BotwSaveManager.Views;
using System;

namespace BotwSaveManager
{
    public partial class App : Application
    {
        public static AppView View { get; set; }
        public static AppViewModel ViewModel { get; set; }
        public static FluentTheme Theme { get; set; } = new(new Uri("avares://BotwActorTool.GUI/Styles"));

        public override void Initialize() => AvaloniaXamlLoader.Load(this);

        public override void OnFrameworkInitializationCompleted()
        {
            // Create default view
            View = new();

            ViewModel = new();
            View.DataContext = ViewModel;

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
                desktop.MainWindow = View;
            }

            Logger.Initialize();
            View.InitializeGenericDialogs();
            base.OnFrameworkInitializationCompleted();
        }
    }
}
