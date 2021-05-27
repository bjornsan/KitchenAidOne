using Newtonsoft.Json;

namespace KitchenAid.Model.Recipes
{
    /// <summary>
    ///   Model for an ingredient in a recipe
    ///   Most attributes in this class are only to manage to deserialize the
    ///   data from spoonacular api.
    /// </summary>
    public class Ingredient
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        [JsonProperty("amount")]
        public double Amount { get; set; }
        /// <summary>Gets or sets the unit.</summary>
        /// <value>The unit.</value>
        [JsonProperty("unit")]
        public string Unit { get; set; }


        /// <summary>Gets or sets the ingredient identifier.</summary>
        /// <value>The ingredient identifier.</value>
        public int IngredientId { get; set; }
        /// <summary>Gets or sets the recipe identifier.</summary>
        /// <value>The recipe identifier.</value>
        public int RecipeId { get; set; }
    }
}
