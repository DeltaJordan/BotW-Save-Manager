using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BotwSaveManager.Core.Helpers
{
    public static class Logger
    {
        private static string SourceRoot = "F:\\GitHub\\BotwSaveManager";

        public static void Initialize()
        {
            TextWriterTraceListener listener = new($".\\{DateTime.Now:yyyy-MM-dd-HH-mm}.log");

            if (Trace.Listeners.Count > 1) {
                Trace.Listeners[1] = listener;
            }
            else {
                Trace.Listeners.Add(listener);
            }

            Trace.AutoFlush = true;
        }

        public static void Write(object msg, [CallerMemberName] string method = "", [CallerFilePath] string filepath = "", [CallerLineNumber] int lineNumber = 0)
        {
            string meta = $"{DateTime.Now:dd:mm:ss:fff} [{method}] | \"{Path.GetRelativePath(SourceRoot, filepath)}\":{lineNumber} | ";
            Trace.WriteLine($"{meta}{msg.ToString()?.Replace("\n", $"\n{new string(' ', meta.Length)}")}".ToCommonPath());
        }
    }
}
