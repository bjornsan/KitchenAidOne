
using KitchenAid.App.Services;
using KitchenAid.App.ViewModels;
using KitchenAid.Model.Inventory;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace KitchenAid.App.Views
{
    public sealed partial class ShoppingListPage : Page
    {
        public ShoppingListViewModel ShoppingListViewModel { get; } = new ShoppingListViewModel();

        public ShoppingListPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                ShoppingListViewModel.CreateShoppingList();
                ShoppingListViewModel.GetCategoriesAsync();
                ShoppingListViewModel.GetAllProductsAsync();
            }
            catch
            {
                UserNotification.NotifyUser("Failed to load shoppinglist properly");
            }
        }

        private async void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            await AddProductsDialog.ShowAsync();
            ShoppingListViewModel.LoadProductsForStorageAsync(ShoppingListViewModel.GetShoppingList().StorageId);
        }

        private void AddToInventoryBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var product in ShoppingListViewModel.Products)
                ShoppingListViewModel.UpdateInventoryAsync(ShoppingListViewModel.GetShoppingList(), product);

            new ToastContentBuilder()
                .SetToastScenario(ToastScenario.Reminder)
                .AddArgument("action", "viewEvent")
                .AddArgument("eventId", 1983)
                .AddText($"{ShoppingListViewModel.Products.Count} products added to inventory.")
                .Show();

            NavigationService.Navigate<InventoryPage>();
        }

        private void AddProductsDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var name = productName.Text;

            int.TryParse(productPrice.Text, out int currentPrice);
            int.TryParse(productQuantity.Text, out int quantity);

            var quantityUnit = productQuantityUnit.Text;
            int category = ((Category)productCategory.SelectedValue).CategoryId;
            var storedIn = (KindOfStorage)productStoredIn.SelectedValue;

            var product = new Product()
            {
                Name = name,
                Quantity = quantity,
                QuantityUnit = quantityUnit,
                CategoryId = category,
                CurrentPrice = currentPrice,
                StoredIn = storedIn,
            };

            ShoppingListViewModel.Products.Add(product);

        }

        private void AddProductsDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        { }

    }
}
