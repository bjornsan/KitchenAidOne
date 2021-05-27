
using KitchenAid.App.Services;
using KitchenAid.App.ViewModels;
using KitchenAid.Model.Inventory;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace KitchenAid.App.Views
{
    public sealed partial class ShoppingListPage : Page
    {
        /// <summary>Gets the shopping ListView model.</summary>
        /// <value>The shopping ListView model.</value>
        public ShoppingListViewModel ShoppingListViewModel { get; } = new ShoppingListViewModel();

        public ShoppingListPage()
        {
            InitializeComponent();
        }

        /// <summary>Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// <br />Loads data neccesary for the view model.</summary>
        /// <param name="e">
        /// Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually the most relevant property to examine is Parameter.
        /// </param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                ShoppingListViewModel.CreateShoppingList();
                ShoppingListViewModel.GetCategoriesAsync();
                ShoppingListViewModel.GetAllProductsAsync();
            }
            catch (Exception ex)
            {
                var logger = new ErrorLogger();
                logger.WriteToFile(ex.Message);

                UserNotification.NotifyUser("Failed to load shoppinglist properly");
            }
        }

        /// <summary>Handles the Click event of the AddProductBtn control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private async void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            await AddProductsDialog.ShowAsync();
            ShoppingListViewModel.LoadProductsForStorageAsync(ShoppingListViewModel.GetShoppingList().StorageId);
        }

        /// <summary>Handles the Click event of the AddToInventoryBtn control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void AddToInventoryBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var product in ShoppingListViewModel.Products)
                ShoppingListViewModel.UpdateInventoryAsync(ShoppingListViewModel.GetShoppingList(), product);

            UserNotification.NotifyUser($"{ShoppingListViewModel.Products.Count} products added to inventory.");

            NavigationService.Navigate<InventoryPage>();
        }

        /// <summary>Adds a product to the storage</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs" /> instance containing the event data.</param>
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
