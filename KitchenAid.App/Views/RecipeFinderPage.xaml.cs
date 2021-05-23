
using KitchenAid.App.ViewModels;

using Windows.UI.Xaml.Controls;

namespace KitchenAid.App.Views
{
    public sealed partial class RecipeFinderPage : Page
    {
        public RecipeFinderViewModel ViewModel { get; } = new RecipeFinderViewModel();

        public RecipeFinderPage()
        {
            InitializeComponent();
        }
    }
}
