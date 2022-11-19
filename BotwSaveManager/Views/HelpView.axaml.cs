using Avalonia.Controls;

namespace BotwSaveManager.Views
{
    public partial class HelpView : Window
    {
        public HelpView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void Quit() => Close();
    }
}
