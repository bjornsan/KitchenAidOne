using Newtonsoft.Json;

namespace KitchenAid.Model.Recipes
{
    public class Ingredient
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("amount")]
        public double Amount { get; set; }
        [JsonProperty("unit")]
        public string Unit { get; set; }


        public int IngredientId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
