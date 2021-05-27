using KitchenAid.Model.Recipes;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KitchenAid.App.DataAccess
{
    /// <summary>Data access for ingredients.</summary>
    class Ingredients
    {
        /// <summary>The HTTP client</summary>
        private readonly HttpClient _httpClient = new HttpClient();
        /// <summary>The base URI</summary>
        static readonly Uri baseUri = new Uri("http://localhost:9420/api/ingredients");


        /// <summary>Gets the ingredients for recipe asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   An array of Type Ingredient.
        /// </returns>
        public async Task<Ingredient[]> GetIngredientsForRecipeAsync(int id)
        {
            var response = await _httpClient.GetAsync(new Uri(baseUri, $"ingredients/{id}/ingredients"));
            string json = await response.Content.ReadAsStringAsync();
            Ingredient[] ingredients = JsonConvert.DeserializeObject<Ingredient[]>(json);
            return ingredients;
        }

        /// <summary>Adds the ingredient asynchronous.</summary>
        /// <param name="ingredient">The ingredient.</param>
        /// <returns>
        ///   A bool if it was successfull or not.
        /// </returns>
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
