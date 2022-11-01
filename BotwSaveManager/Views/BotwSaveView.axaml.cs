using Avalonia.Controls;
using BotwSaveManager.Core;
using BotwSaveManager.ViewModels;

namespace BotwSaveManager.Views
{
    public partial class BotwSaveView : UserControl
    {
        public BotwSaveView() => InitializeComponent();
        public BotwSaveView(BotwSave save)
        {
            InitializeComponent();
            DataContext = new BotwSaveViewModel(save);
        }
    }
}
