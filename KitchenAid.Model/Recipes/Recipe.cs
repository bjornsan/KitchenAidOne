using Newtonsoft.Json;
using System.Collections.Generic;

namespace KitchenAid.Model.Recipes
{
    public class Recipe
    {
        // Properties used to deserialize the JSON object.

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("missedIngredients")]
        public ICollection<Ingredient> MissedIngredients = new List<Ingredient>();
        [JsonProperty("usedIngredients")]
        public ICollection<Ingredient> UsedIngredients = new List<Ingredient>();


        // Properties used to handle actions aginst the database with Entity Framework
        public int RecipeId { get; set; }
        public ICollection<Ingredient> Ingredients = new List<Ingredient>();
    }
}
