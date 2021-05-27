using KitchenAid.App.DataAccess;
using KitchenAid.App.Helpers;
using KitchenAid.App.Services;
using KitchenAid.Model.Recipes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
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

        private int alreadyCalled = 0;

        private readonly Recipes recipeDataAccess = new Recipes();
        private readonly Ingredients ingredientDataAccess = new Ingredients();

        private Recipe selectedRecipe;

        public Recipe SelectedRecipe
        {
            get => selectedRecipe;
            set
            {
                Set(ref selectedRecipe, value);

                if (value != null)
                    GetRecipeInformationAsync(((Recipe)value).Id, selectedRecipe, 0);

            }
        }

        private Recipe selectedFavorite;

        public Recipe SelectedFavorite
        {
            get => selectedFavorite;
            set
            {
                Set(ref selectedFavorite, value);

                if (value != null)
                    GetRecipeInformationAsync(((Recipe)value).Id, selectedFavorite, 1);
            }
        }


        public RecipeFinderViewModel()
        {
            AddCommand = new RelayCommand<Recipe>(async recipe =>
            {

                if (recipe == null)
                {
                    UserNotification.NotifyUser("Recipe not found");
                }

                try
                {
                    if (await recipeDataAccess.AddRecipeAsync(recipe))
                    {
                        foreach (Ingredient ingredient in Ingredients)
                        {
                            var newIngredient = new Ingredient()
                            {
                                Name = ingredient.Name,
                                Amount = ingredient.Amount,
                                Unit = ingredient.Unit,
                                IngredientId = ingredient.IngredientId,
                                RecipeId = recipe.Id
                            };

                            await ingredientDataAccess.AddIngredientAsync(newIngredient);
                        }
                        Favorites.Add(recipe);
                    }
                }
                catch
                {
                    UserNotification.NotifyUser("Failed to add Recipe");
                }
            });

            DeleteCommand = new RelayCommand<Recipe>(async param =>
            {
                try
                {
                    if (await recipeDataAccess.DeleteRecipeAsync((Recipe)param))
                        Favorites.Remove(param);
                }
                catch
                {
                    UserNotification.NotifyUser("Failed to delete recipe.");
                }

            }, param => param != null);

        }

        public async Task FindRecipesAsync(CancellationTokenSource cts, string[] ingredients = null)
        {
            if (ingredients == null)
                ingredients = new string[] { "tomato", "pasta", "chicken" };

            IEnumerable<Recipe> recipes = null; ;
            try
            {
                recipes = await recipeDataAccess.FindRecipiesAsync(ingredients);
            }
            catch
            {
                UserNotification.NotifyUser("Could not load recipes, try again");
            }

            if (recipes != null)
            {
                Recipes.Clear();

                foreach (var recipe in recipes)
                    Recipes.Add(recipe);
            }

            cts.Token.ThrowIfCancellationRequested();

        }


        // Token: 0 = selectedRecipe, 1 = selectedFavorite
        // Mega bug, when awaiting ingredients in the if block where token = 1, it jumps back to the beginning of the method and
        // excecutes the method once more, thus resulting in that the ingredients are loaded twice.
        public async void GetRecipeInformationAsync(int id, Recipe selectedRecipe, int token)
        {

            if (id > 0)
            {
                if (alreadyCalled == 2)
                    alreadyCalled = 0;

                Ingredients.Clear();
                Instructions.Clear();

                GetInstructionsAsync(id);

                if (token == 0)
                {
                    foreach (var ingredient in selectedRecipe.MissedIngredients)
                        Ingredients.Add(ingredient);

                    foreach (var uingredient in selectedRecipe.UsedIngredients)
                        Ingredients.Add(uingredient);
                }
                else if (token == 1)
                {
                    alreadyCalled++;
                    if (alreadyCalled == 1)
                    {
                        var ingredients = await ingredientDataAccess.GetIngredientsForRecipeAsync(id);

                        foreach (var ingredient in ingredients)
                            Ingredients.Add(ingredient);

                    }
                }
            }
        }

        public async void GetInstructionsAsync(int id)
        {
            Instructions.Clear();

            try
            {
                var instruction = await recipeDataAccess.GetRecipeInformationAsync(id);

                if (instruction != null && instruction.Instructions != null)
                {
                    string instructionsReformatted = instruction.Instructions.Replace("<p>", "");
                    string[] instructionSteps = instructionsReformatted.Split("</p>");

                    foreach (var sub in instructionSteps)
                    {
                        var trimmed = sub.Trim();

                        if (!string.IsNullOrEmpty(trimmed))
                            Instructions.Add(trimmed);
                    }
                }
            }
            catch
            {
                UserNotification.NotifyUser("Could not load instructions");
            }
        }

        public async void GetFavoritesAsync()
        {
            try
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
            catch
            {
                UserNotification.NotifyUser("Faild to load favoirtes.");
            }

        }

        public void SearchCanceled()
        {
            Recipes.Clear();
            Instructions.Clear();
            Ingredients.Clear();

            UserNotification.NotifyUser("Search for recipes was cancelled");
        }
    }
}
