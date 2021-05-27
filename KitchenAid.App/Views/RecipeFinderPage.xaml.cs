
using KitchenAid.App.Services;
using KitchenAid.App.ViewModels;
using System;
using System.Threading;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace KitchenAid.App.Views
{
    public sealed partial class RecipeFinderPage : Page
    {
        /// <summary>Gets the recipe finder view model.</summary>
        /// <value>The recipe finder view model.</value>
        public RecipeFinderViewModel RecipeFinderViewModel { get; } = new RecipeFinderViewModel();

        /// <summary>The cancelation token</summary>
        private CancellationTokenSource cts;

        public RecipeFinderPage()
        {
            InitializeComponent();
        }

        /// <summary>Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// <br />Loads the favorites to the viewmodel.</summary>
        /// <param name="e">
        /// Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually the most relevant property to examine is Parameter.
        /// </param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RecipeFinderViewModel.GetFavoritesAsync();
        }


        /// <summary>Handles the Click event of the FindRecipe control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs" /> instance containing the event data.</param>
        private async void FindRecipe_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var listviewItems = lvSearchIngredients.SelectedItems;

            var selectedIngredients = new string[listviewItems.Count];

            var count = 0;
            foreach (string ingredient in lvSearchIngredients.SelectedItems)
            {
                selectedIngredients[count] = ingredient;
                count++;
            }

            cts = new CancellationTokenSource();

            try
            {
                await RecipeFinderViewModel.FindRecipesAsync(cts, selectedIngredients);
            }
            catch (OperationCanceledException ex)
            {
                RecipeFinderViewModel.SearchCanceled();
                var logger = new ErrorLogger();
                logger.WriteToFile(ex.Message);
            }
        }

        /// <summary>Handles the Click event of the CancelBtn control.
        /// <br />Invoked when the search command is cancelled.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs" /> instance containing the event data.</param>
        private void CancelBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            cts.Cancel();
        }
    }
}
