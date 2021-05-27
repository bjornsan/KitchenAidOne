
using KitchenAid.App.ViewModels;
using System;
using System.Threading;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace KitchenAid.App.Views
{
    public sealed partial class RecipeFinderPage : Page
    {
        public RecipeFinderViewModel RecipeFinderViewModel { get; } = new RecipeFinderViewModel();

        private CancellationTokenSource cts;

        public RecipeFinderPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RecipeFinderViewModel.GetFavoritesAsync();
        }

        // Handles the selection of
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
                // log error
            }
        }

        private void CancelBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            cts.Cancel();
        }
    }
}
