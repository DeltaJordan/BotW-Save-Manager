using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BotwSaveManager.Core.Helpers
{
    public static class Logger
    {
        private static readonly string SourceRoot = "F:\\GitHub\\BotwSaveManager";

        public static string? CurrentLog { get; set; }

        public static void Initialize(TraceListener? customListener = null)
        {
            Directory.CreateDirectory("./Logs");
            CurrentLog = $"./Logs/{DateTime.Now:yyyy-MM-dd-HH-mm}.log";
            TextWriterTraceListener listener = new(CurrentLog);

            AddTraceListener(listener, 1);

            if (customListener != null) {
                AddTraceListener(customListener, 2);
            }

            Trace.AutoFlush = true;
        }

        public static void Write(object msg, [CallerMemberName] string method = "", [CallerFilePath] string filepath = "", [CallerLineNumber] int lineNumber = 0)
        {
            string meta = $"{DateTime.Now:dd:mm:ss:fff} [{method}] | \"{Path.GetRelativePath(SourceRoot, filepath)}\":{lineNumber} | ";
            Trace.WriteLine($"{meta}{msg.ToString()?.Replace("\n", $"\n{new string(' ', meta.Length)}")}".ToCommonPath());
        }

        private static void AddTraceListener(TraceListener listener, int pos)
        {
            if (Trace.Listeners.Count > pos) {
                Trace.Listeners[pos] = listener;
            }
            else {
                Trace.Listeners.Add(listener);
            }
        }
    }
}
