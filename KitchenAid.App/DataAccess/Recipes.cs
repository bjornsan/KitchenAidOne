using KitchenAid.Model.Recipes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KitchenAid.App.DataAccess
{
    public class Recipes
    {
        readonly HttpClient _httpClient = new HttpClient();
        static readonly Uri baseUri = new Uri("http://localhost:59283/api/recipes");

        // Requests from Spoonacular API
        // apiKey=429d6ef10f5b42c5866251adb3e9d796
        static readonly string requestUri = "https://api.spoonacular.com/recipes/findByIngredients?apiKey=429d6ef10f5b42c5866251adb3e9d796&ingredients=";


        public Recipes()
        {}

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
                var response = await _httpClient.GetAsync(new Uri(request.ToString()));
                string json = await response.Content.ReadAsStringAsync();
                Recipe[] recipes = JsonConvert.DeserializeObject<Recipe[]>(json);
                return recipes;
            }
        }

        public async Task<IEnumerable<Recipe>> GetRecipeInformationAsync(int id)
        {
            // TODO: THIS IS JUST BOILER PLATE CODE - REPLACE WITH REAL CODE
            var response = await _httpClient.GetAsync(baseUri);
            string json = await response.Content.ReadAsStringAsync();
            Recipe[] recipes = JsonConvert.DeserializeObject<Recipe[]>(json);
            return recipes;
        }
    }
}
