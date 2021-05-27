using KitchenAid.App.Services;
using KitchenAid.Model.Recipes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KitchenAid.App.DataAccess
{
    /// <summary>Data access for recipes.</summary>
    public class Recipes
    {
        /// <summary>The base URI</summary>
        private readonly Uri baseUri = new Uri("http://localhost:9420/api/recipes");

        // Requests from Spoonacular API
        // apiKey=429d6ef10f5b42c5866251adb3e9d796
        /// <summary>The request URI</summary>
        private readonly string requestUri = @"https://api.spoonacular.com/recipes/findByIngredients?apiKey=429d6ef10f5b42c5866251adb3e9d796&ingredients=";

        public Recipes()
        { }

        /// <summary>Finds the recipies asynchronous.
        /// Given a list of ingredients.<br /><br />The request is then built with the stringbuilder.</summary>
        /// <param name="ingredients">The ingredients.</param>
        /// <returns>
        ///   An array of Type Recipe, if found any.
        /// </returns>
        public async Task<IEnumerable<Recipe>> FindRecipiesAsync(string[] ingredients)
        {
            var request = new StringBuilder(requestUri, 50);
            int upperBound = ingredients.GetUpperBound(0);

            foreach (var ingredient in ingredients)
            {
                if (ingredient == ingredients[upperBound])
                    request.Append(ingredient);
                else
                    request.Append($"{ingredient}+");
            }

            using (var _httpClient = new HttpClient())
            {
                try
                {
                    var response = await _httpClient.GetAsync(new Uri(request.ToString()));
                    string json = await response.Content.ReadAsStringAsync();
                    Recipe[] recipes = JsonConvert.DeserializeObject<Recipe[]>(json);
                    return recipes;
                }
                catch (Exception ex)
                {
                    var logger = new ErrorLogger();
                    logger.WriteToFile(ex.Message);
                    throw;
                }
            }
        }

        /// <summary>Gets the recipe information asynchronous from the spoonacular api.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   returns an object of Type Instruction, containing the instructions for the recipe.
        /// </returns>
        public async Task<Instruction> GetRecipeInformationAsync(int id)
        {
            var requestUri = $"https://api.spoonacular.com/recipes/{id}/information?includeInstructions=true&&apiKey=429d6ef10f5b42c5866251adb3e9d796";

            using (var _httpClient = new HttpClient())
            {
                try
                {
                    var response = await _httpClient.GetAsync(new Uri(requestUri));
                    string json = await response.Content.ReadAsStringAsync();
                    Instruction instructions = JsonConvert.DeserializeObject<Instruction>(json);
                    return instructions;
                }
                catch (Exception ex)
                {
                    var logger = new ErrorLogger();
                    logger.WriteToFile(ex.Message);
                    throw;
                }
            }
        }

        /// <summary>Adds the recipe asynchronous.</summary>
        /// <param name="recipe">The recipe.</param>
        /// <returns>
        ///   A bool if it was successfull or not.
        /// </returns>
        internal async Task<bool> AddRecipeAsync(Recipe recipe)
        {
            string json;
            HttpResponseMessage result;

            using (var _httpClient = new HttpClient())
            {
                try
                {
                    json = JsonConvert.SerializeObject(recipe);
                    result = await _httpClient.PostAsync(baseUri, new StringContent(json, Encoding.UTF8, "application/json"));
                }
                catch (Exception ex)
                {
                    var logger = new ErrorLogger();
                    logger.WriteToFile(ex.Message);
                    throw;
                }

                if (result.IsSuccessStatusCode)
                {
                    json = await result.Content.ReadAsStringAsync();
                    var returnedRecipe = JsonConvert.DeserializeObject<Recipe>(json);
                    recipe.RecipeId = returnedRecipe.RecipeId;

                    return true;
                }
                else
                    return false;
            }
        }

        /// <summary>Gets the favorites asynchronous from the database.</summary>
        /// <returns>
        ///   An array of type Recipe.
        /// </returns>
        internal async Task<IEnumerable<Recipe>> GetFavoritesAsync()
        {
            using (var _httpClient = new HttpClient())
            {
                try
                {
                    var response = await _httpClient.GetAsync(new Uri(baseUri.ToString()));
                    string json = await response.Content.ReadAsStringAsync();
                    Recipe[] favorites = JsonConvert.DeserializeObject<Recipe[]>(json);
                    return favorites;
                }
                catch (Exception ex)
                {
                    var logger = new ErrorLogger();
                    logger.WriteToFile(ex.Message);
                    throw;
                }
            }
        }

        /// <summary>Deletes the recipe asynchronous.</summary>
        /// <param name="recipe">The recipe.</param>
        /// <returns>
        ///   A bool if it was successfull or not.
        /// </returns>
        internal async Task<bool> DeleteRecipeAsync(Recipe recipe)
        {
            using (var _httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage result = await _httpClient.DeleteAsync(new Uri(baseUri, $"recipes/{recipe.RecipeId}"));
                    return result.IsSuccessStatusCode;
                }
                catch (Exception ex)
                {
                    var logger = new ErrorLogger();
                    logger.WriteToFile(ex.Message);
                    throw;
                }
            }
        }
    }
}
