using KitchenAid.Model.Recipes;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KitchenAid.App.DataAccess
{
    class Ingredients
    {
        readonly HttpClient _httpClient = new HttpClient();
        static readonly Uri baseUri = new Uri("http://localhost:9420/api/ingredients");


        public async Task<Ingredient[]> GetIngredientsForRecipeAsync(int id)
        {
            var response = await _httpClient.GetAsync(new Uri(baseUri, $"ingredients/{id}/ingredients"));
            string json = await response.Content.ReadAsStringAsync();
            Ingredient[] ingredients = JsonConvert.DeserializeObject<Ingredient[]>(json);
            return ingredients;
        }

        internal async Task<bool> AddIngredientAsync(Ingredient ingredient)
        {
            string json = JsonConvert.SerializeObject(ingredient);
            var result = await _httpClient.PostAsync(baseUri, new StringContent(json, Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
            {
                json = await result.Content.ReadAsStringAsync();
                var returnedIngredient = JsonConvert.DeserializeObject<Ingredient>(json);
                ingredient.IngredientId = returnedIngredient.IngredientId;

                return true;
            }
            else
                return false;
        }
    }
}
