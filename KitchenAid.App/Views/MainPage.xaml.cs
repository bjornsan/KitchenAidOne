
using KitchenAid.App.ViewModels;

using Windows.UI.Xaml.Controls;

namespace KitchenAid.App.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
