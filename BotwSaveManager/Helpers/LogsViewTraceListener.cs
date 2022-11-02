using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwSaveManager.Helpers
{
    internal class LogsViewTraceListener : TraceListener
    {
        private readonly Action<string> WriteToBase;

        public LogsViewTraceListener(Action<string> writeToBase)
        {
            Name = nameof(LogsViewTraceListener);
            WriteToBase = writeToBase;
        }

        public override void Write(string? message)
        {
            if (message != null) {
                WriteToBase(message);
            }
        }

        public override void WriteLine(string? message)
        {
            if (message != null) {
                WriteToBase(message);
            }
        }
    }
}
