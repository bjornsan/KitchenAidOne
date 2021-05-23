
using KitchenAid.App.ViewModels;
using KitchenAid.Model.Inventory;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
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
            int? returnCode = null;
            try
            {
                returnCode = ShoppingListViewModel.CreateShoppingList();
                ShoppingListViewModel.GetCategoriesAsync();
                ShoppingListViewModel.GetAllProductsAsync();
            }
            catch
            {

            }

            if (returnCode != null)
            {
                new ToastContentBuilder()
                    .AddArgument("action", "viewEvent")
                    .AddArgument("eventId", 1983)
                    .AddText($"Method call ended with code: {returnCode}")
                    .AddText($"sID: {ShoppingListViewModel.GetShoppingList().StorageId}")
                    .Show();
            }
            else
            {


            }


        }

        private void AddProductsDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            //TODO: Make something useful
            // This is all just temporary code to check that the dataflow is working before goint to deep down in the rabbit hole.

            var name = productName.Text;

            int.TryParse(productPrice.Text, out int currentPrice);
            int.TryParse(productQuantity.Text, out int quantity);

            var quantityUnit = productQuantityUnit.Text;
            int category = ((Category)productCategory.SelectedValue).CategoryId;
            var storedIn = (KindOfStorage)productStoredIn.SelectedValue;


            var shoppingList = ShoppingListViewModel.GetShoppingList();

            var product = new Product()
            {
                Name = name,
                Quantity = quantity,
                QuantityUnit = quantityUnit,
                CategoryId = category,
                CurrentPrice = currentPrice,
                StoredIn = storedIn,
            };

            ShoppingListViewModel.UpdateInventoryAsync(shoppingList, product);


            //shoppingList.Products.Add(new StorageProduct() { Storage = shoppingList, Product = salmon });
            //ShoppingListViewModel.LoadProductsForStorageAsync(shoppingList.StorageId);

            //ShoppingListViewModel.AddProductCommand.Execute(ShoppingListViewModel.Products);


            //new ToastContentBuilder()
            //    .SetToastScenario(ToastScenario.Reminder)
            //    .AddArgument("action", "viewEvent")
            //    .AddArgument("eventId", 1983)
            //    .AddText($"{shoppingList.Products.Count} products added")
            //    .AddText($"{p.Name} -- {p.Quantity} {p.QuantityUnit} -- {p.Category.Name}")
            //    .Show();

        }

        private void AddProductsDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private async void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            await AddProductsDialog.ShowAsync();
            ShoppingListViewModel.LoadProductsForStorageAsync(ShoppingListViewModel.GetShoppingList().StorageId);
        }

        private void AddToInventoryBtn_Click(object sender, RoutedEventArgs e)
        {
            new ToastContentBuilder()
                .SetToastScenario(ToastScenario.Reminder)
                .AddArgument("action", "viewEvent")
                .AddArgument("eventId", 1983)
                .AddText($"Yes, you pressed the Add to Inventory button.")
                .Show();
        }

        private async void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            var selected = ShoppingListViewModel.SelectedProduct;
            await UpdateProductsDialog.ShowAsync();
        }

        private void UpdateProductsDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }

        private void UpdateProductsDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
    }
}
