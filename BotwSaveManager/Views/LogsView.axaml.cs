using Avalonia.Controls;
using Avalonia.Threading;
using BotwSaveManager.Core.Helpers;
using BotwSaveManager.ViewModels;
using System;
using System.Diagnostics;
using System.IO;

namespace BotwSaveManager.Views
{
    public partial class LogsView : UserControl
    {
        public LogsView()
        {
            InitializeComponent();
            DataContext = new LogsViewModel();
            LogTraceBox.AutoScrollToSelectedItem = true;
        }

        public void Load()
        {
            ViewModel.Content = this;
            LogTraceBox.ScrollIntoView(LogTraceBox.ItemCount - 1);
        }

        public void Write(string message)
        {
            Dispatcher.UIThread.InvokeAsync(() => {
                int idx = message.LastIndexOf('|');
                string meta = message[..idx];
                string msg = message[(idx + 1)..].Replace(new string(' ', meta.Length+2), "");
                (DataContext as LogsViewModel)!.LogTrace.Add(new(meta.Trim(), msg.Trim()));
                LogTraceBox.ScrollIntoView(LogTraceBox.ItemCount - 1);
            });
        }
    }
}
