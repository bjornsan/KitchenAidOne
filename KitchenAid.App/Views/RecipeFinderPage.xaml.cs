
using KitchenAid.App.ViewModels;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace KitchenAid.App.Views
{
    public sealed partial class RecipeFinderPage : Page
    {
        public RecipeFinderViewModel RecipeFinderViewModel { get; } = new RecipeFinderViewModel();

        public RecipeFinderPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RecipeFinderViewModel.GetFavoritesAsync();
        }

        private void TestButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {


            var listviewItems = lvSearchIngredients.SelectedItems;

            var selectedIngredients = new string[listviewItems.Count];

            var count = 0;
            foreach (string ingredient in lvSearchIngredients.SelectedItems)
            {
                selectedIngredients[count] = ingredient;
                count++;
            }

            RecipeFinderViewModel.FindRecipes(selectedIngredients);
        }
    }
}
