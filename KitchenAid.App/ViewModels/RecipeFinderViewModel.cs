
using KitchenAid.App.DataAccess;
using KitchenAid.App.Helpers;
using KitchenAid.Model.Recipes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KitchenAid.App.ViewModels
{
    public class RecipeFinderViewModel : Observable
    {
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ObservableCollection<Recipe> Recipes { get; } = new ObservableCollection<Recipe>();
        public ObservableCollection<Recipe> Favorites { get; } = new ObservableCollection<Recipe>();
        public ObservableCollection<Ingredient> Ingredients { get; } = new ObservableCollection<Ingredient>();
        public ObservableCollection<String> Instructions { get; } = new ObservableCollection<String>();
        public ObservableCollection<String> SearchIngredients { get; } = new ObservableCollection<String>()
        {
            "tomato", "paprika", "pepper", "cucumber", "chili", "corn", "beans", "pasta", "rice", "fish", "pork", "ox", "deer", "chicken", "cheese", "milk",
            "yoghurt", "onion"
        };
        


        private readonly Recipes recipeDataAccess = new Recipes();

        private Recipe selectedRecipe;

        public Recipe SelectedRecipe
        {
            get => selectedRecipe;
            set
            {
                Set(ref selectedRecipe, value);

                if (value != null)
                    GetRecipeInformationAsync(((Recipe)value).Id);

            }
        }

        private Recipe selectedFavorite;

        public Recipe SelectedFavorite
        {
            get => selectedFavorite;
            set
            {
                Set(ref selectedFavorite, value);
            }
        }


        public RecipeFinderViewModel()
        {
            AddCommand = new RelayCommand<Recipe>(async recipe =>
            {
                if (recipe == null)
                {
                    // TODO: LOG? MAYBE DO SOME OTHER STUFF. FIX ME.
                    Console.WriteLine("NOT FOUND");
                }

                if (await recipeDataAccess.AddRecipeAsync(recipe))
                    Favorites.Add(recipe);
            });

            DeleteCommand = new RelayCommand<Recipe>(async param =>
            {
                if (await recipeDataAccess.DeleteRecipeAsync((Recipe)param))
                    Favorites.Remove(param);
            }, param => param != null);

        }

        public async void FindRecipes(string[] ingredients = null)
        {
            if (ingredients == null)
                ingredients = new string[]{ "tomato", "pasta", "chicken" };

            var recipes = await recipeDataAccess.FindRecipiesAsync(ingredients);

            Recipes.Clear();

            foreach (var recipe in recipes)
                Recipes.Add(recipe);
        }

        public void GetRecipeInformationAsync(int id)
        {
            if (id < 0)
                return;

            GetInstructionsAsync(id);

            Ingredients.Clear();

            foreach (var ingredient in SelectedRecipe.MissedIngredients)
                Ingredients.Add(ingredient);

            foreach (var uingredient in SelectedRecipe.UsedIngredients)
                Ingredients.Add(uingredient);

        }

        public async void GetInstructionsAsync(int id)
        {
            Instructions.Clear();

            var instructions = await recipeDataAccess.GetRecipeInformationAsync(id);

            if (instructions.Instructions != null)
            {
                string instructionsReformatted = instructions.Instructions.Replace("<p>", "");
                string[] instructionSteps = instructionsReformatted.Split("</p>");

                foreach (var sub in instructionSteps)
                {
                    var trimmed = sub.Trim();

                    if (!string.IsNullOrEmpty(trimmed))
                        Instructions.Add(trimmed);
                }

            }

        }

        public async void GetFavoritesAsync()
        {
            var favorites = await recipeDataAccess.GetFavoritesAsync();

            if (favorites != null)
            {
                foreach (Recipe recipe in favorites)
                {
                    Favorites.Add(recipe);
                }
            }
        }
    }
}
