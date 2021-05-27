
using KitchenAid.App.Services;
using KitchenAid.App.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace KitchenAid.App.Views
{
    public sealed partial class InventoryPage : Page
    {
        /// <summary>Gets the inventory view model.</summary>
        /// <value>The inventory view model.</value>
        public InventoryViewModel InventoryViewModel { get; } = new InventoryViewModel();

        public InventoryPage()
        {
            InitializeComponent();
        }
        /// <summary>Invoked when the Page is loaded and becomes the current source of a parent Frame. This loads the data into the view model.</summary>
        /// <param name="e">
        /// Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually the most relevant property to examine is Parameter.
        /// </param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await InventoryViewModel.LoadDataAsync();
        }

        /// <summary>When the new shopping list button is clicked this sends the user to the shoppinglist page</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs" /> instance containing the event data.</param>
        private void NewShoppingListBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NavigationService.Navigate<ShoppingListPage>();
        }
    }
}
