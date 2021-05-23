
using KitchenAid.App.Services;
using KitchenAid.App.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace KitchenAid.App.Views
{
    public sealed partial class InventoryPage : Page
    {
        public InventoryViewModel InventoryViewModel { get; } = new InventoryViewModel();

        public InventoryPage()
        {
            InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await InventoryViewModel.LoadDataAsync();
        }

        private void NewShoppingListBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate<ShoppingListPage>();
        }


        private void UpdateList_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var storageToUpdate = InventoryViewModel.SelectedStorage;
            InventoryViewModel.SelectedStorage.KindOfStorage = Model.Inventory.KindOfStorage.Fridge;
            InventoryViewModel.UpdateCommand.Execute(storageToUpdate);
        }
    }
}
