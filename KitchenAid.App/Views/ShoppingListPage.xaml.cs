
using KitchenAid.App.ViewModels;

using Windows.UI.Xaml.Controls;

namespace KitchenAid.App.Views
{
    public sealed partial class ShoppingListPage : Page
    {
        public ShoppingListViewModel ViewModel { get; } = new ShoppingListViewModel();

        public ShoppingListPage()
        {
            InitializeComponent();
        }
    }
}
