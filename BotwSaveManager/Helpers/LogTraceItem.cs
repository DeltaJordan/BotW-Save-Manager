using ReactiveUI;

namespace BotwSaveManager.Helpers
{
    public class LogTraceItem : ReactiveObject
    {
        private string meta;
        public string Meta {
            get => meta;
            set => this.RaiseAndSetIfChanged(ref meta, value);
        }

        private string message;
        public string Message {
            get => message;
            set => this.RaiseAndSetIfChanged(ref message, value);
        }

        public LogTraceItem(string meta, string message)
        {
            this.meta = meta;
            this.message = message;
        }
    }
}
