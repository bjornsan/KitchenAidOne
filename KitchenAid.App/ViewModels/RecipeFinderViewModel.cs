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
    /// <summary>The View Model for the Recipe FInder</summary>
    public class RecipeFinderViewModel : Observable
    {
        /// <summary>Gets or sets the add command.</summary>
        /// <value>The add command.</value>
        public ICommand AddCommand { get; set; }
        /// <summary>Gets or sets the delete command.</summary>
        /// <value>The delete command.</value>
        public ICommand DeleteCommand { get; set; }
        /// <summary>Recipes found in the search.</summary>
        /// <value>The recipes.</value>
        public ObservableCollection<Recipe> Recipes { get; } = new ObservableCollection<Recipe>();
        /// <summary>Recipes used for favorites</summary>
        /// <value>The favorites.</value>
        public ObservableCollection<Recipe> Favorites { get; } = new ObservableCollection<Recipe>();
        /// <summary>Ingredients for a spesific recipe.</summary>
        /// <value>The ingredients.</value>
        public ObservableCollection<Ingredient> Ingredients { get; } = new ObservableCollection<Ingredient>();
        /// <summary>Instructions for a spesific recipe.</summary>
        /// <value>The instructions.</value>
        public ObservableCollection<String> Instructions { get; } = new ObservableCollection<String>();
        /// <summary>Possible ingredients to search for. Feel free to add more.</summary>
        /// <value>The search ingredients.</value>
        public ObservableCollection<String> SearchIngredients { get; } = new ObservableCollection<String>()
        {
            "tomato", "paprika", "pepper", "cucumber", "chili", "corn", "beans", "pasta", "rice", "fish", "pork", "ox", "deer", "chicken", "cheese", "milk",
            "yoghurt", "onion", "Chocolate", "Honey", "Sugar", "Wine"
        };

        /// <summary>Used to fix a bug in GetRecipeInformationAsync.
        /// See more under GetRecipeInformationAsync.</summary>
        private int alreadyCalled = 0;

        /// <summary>The recipe data access</summary>
        private readonly Recipes recipeDataAccess = new Recipes();
        /// <summary>The ingredient data access</summary>
        private readonly Ingredients ingredientDataAccess = new Ingredients();

        /// <summary>The selected recipe</summary>
        private Recipe selectedRecipe;

        /// <summary>Gets or sets the selected recipe.</summary>
        /// <value>The selected recipe.</value>
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

        /// <summary>The selected favorite. Used in the favorites tab of the Recipe Finder UI.</summary>
        private Recipe selectedFavorite;

        /// <summary>Gets or sets the selected favorite.</summary>
        /// <value>The selected favorite.</value>
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

        /// <summary>Initializes a new instance of the <see cref="RecipeFinderViewModel" /> class.</summary>
        public RecipeFinderViewModel()
        {
            // Gets a recipe, If it manages to store the recipe, it will continue to
            // try to store all of its ingredients.
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

        /// <summary>Finds the recipes asynchronous.</summary>
        /// <param name="cts">The CTS.</param>
        /// <param name="ingredients">The ingredients.</param>
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



        /// <summary>Gets the recipe information asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="selectedRecipe">The selected recipe.</param>
        /// <param name="token">The token. Token: 0 = selectedRecipe, 1 = selectedFavorite</param>
        /// 
        // Mega bug, when awaiting ingredients in the if block where token = 1, it jumps back to the beginning of the method and
        // excecutes the method once more, thus resulting in that the ingredients are loaded twice.
        // Using the instancevariable alreadyCalled to solve this problem. By incrementing this variable every
        // time the method is called it will ensure that only the first call will exceute.
        // Further checking if the alreadyCalled == 2, ensures that the method will execute by the next time it is called
        // for a new recipe.

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

        /// <summary>Gets the cooking instructions asynchronous. And tries a small atempt on normalizing the output.</summary>
        /// <param name="id">The identifier.</param>
        public async void GetInstructionsAsync(int id)
        {
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

        /// <summary>Gets the favorite recipes asynchronous.</summary>
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

        /// <summary>
        /// Called after the FindRecipesAsync has been cancelled by the cancellation token.
        /// Clears the lists containing the recipes found. And sends a notification to the user
        /// that the search has been cancelled.
        /// </summary>
        public void SearchCanceled()
        {
            Recipes.Clear();
            Instructions.Clear();
            Ingredients.Clear();

            UserNotification.NotifyUser("Search for recipes was cancelled");
        }
    }
}
