
using KitchenAid.App.ViewModels;

using Windows.UI.Xaml.Controls;

namespace KitchenAid.App.Views
{
    public sealed partial class RecipeFinderPage : Page
    {
        public RecipeFinderViewModel RecipeFinderViewModel { get; } = new RecipeFinderViewModel();

        public RecipeFinderPage()
        {
            InitializeComponent();
        }

        private void TestButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            RecipeFinderViewModel.FindRecipes();
        }
    }
}
