
using KitchenAid.App.DataAccess;
using KitchenAid.App.Helpers;
using KitchenAid.Model.Recipes;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace KitchenAid.App.ViewModels
{
    public class RecipeFinderViewModel : Observable
    {
        public ICommand AddCommand { get; set; }
        public ObservableCollection<Recipe> Recipes { get; } = new ObservableCollection<Recipe>();
        public ObservableCollection<Recipe> Favorites { get; } = new ObservableCollection<Recipe>();
        public ObservableCollection<Ingredient> Ingredients { get; } = new ObservableCollection<Ingredient>();

        private readonly Recipes recipeDataAccess = new Recipes();

        private Recipe selectedRecipe;

        public Recipe SelectedRecipe
        {
            get => selectedRecipe;
            set
            {
                Set(ref selectedRecipe, value);

                if (value != null)
                    LoadRecipeInformationAsync(((Recipe)value).RecipeId);

            }
        }


        public RecipeFinderViewModel()
        {
            AddCommand = new RelayCommand<Recipe>(async recipe =>
            {
                if (recipe == null)
                {
                    Console.WriteLine("NOT FOUND");
                }

                if (await recipeDataAccess.AddRecipeAsync(recipe))
                    Favorites.Add(recipe);
            });
        }

        public async void FindRecipes(string[] ingredients = null)
        {
            if (ingredients == null)
                ingredients = new string[] { "tomato", "pasta", "chicken" };

            var recipes = await recipeDataAccess.FindRecipiesAsync(ingredients);

            Recipes.Clear();

            foreach (var recipe in recipes)
                Recipes.Add(recipe);
        }

        public void LoadRecipeInformationAsync(int id)
        {
            if (id < 0)
                return;

            //await recipeDataAccess.GetRecipeInformationAsync(id);

            Ingredients.Clear();

            foreach (var ingredient in SelectedRecipe.MissedIngredients)
                Ingredients.Add(ingredient);

            foreach (var uingredient in SelectedRecipe.UsedIngredients)
                Ingredients.Add(uingredient);

        }

    }
}
