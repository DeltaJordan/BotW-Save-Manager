using Avalonia;
using BotwSaveManager.Helpers;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwSaveManager.ViewModels
{
    public class LogsViewModel : ReactiveObject
    {
        private LogTraceItem selected = new("[null]", "[null]");
        public LogTraceItem Selected {
            get => selected;
            set => this.RaiseAndSetIfChanged(ref selected, value);
        }

        private ObservableCollection<LogTraceItem> logTrace = new();
        public ObservableCollection<LogTraceItem> LogTrace {
            get => logTrace;
            set => this.RaiseAndSetIfChanged(ref logTrace, value);
        }

        public async void Copy()
        {
            await Application.Current!.Clipboard!.SetTextAsync($"{Selected.Meta} | {Selected.Message}");
        }
    }
}
