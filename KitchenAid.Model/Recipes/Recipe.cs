using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KitchenAid.Model.Recipes
{
    /// <summary>
    ///   Models a recipe, after the way the spoonacular api is buildt.
    ///   Most attributes in this class are only to manage to deserialize the
    ///   data from spoonacular api.
    /// </summary>
    public class Recipe
    {
        /// <summary>Gets or sets the recipe identifier from the spoonacular api.</summary>
        /// <value>The identifier.</value>
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        [JsonProperty("title")]
        public string Title { get; set; }
        /// <summary>Gets or sets the image path</summary>
        /// <value>The image.</value>
        [JsonProperty("image")]
        public string Image { get; set; }

        /// <summary>The missed ingredients</summary>
        [JsonProperty("missedIngredients")]
        public ICollection<Ingredient> MissedIngredients = new List<Ingredient>();
        /// <summary>The used ingredients</summary>
        [JsonProperty("usedIngredients")]
        public ICollection<Ingredient> UsedIngredients = new List<Ingredient>();



        /// <summary>Gets or sets the recipe identifier.</summary>
        /// <value>The recipe identifier.</value>
        [Key]
        public int RecipeId { get; set; }
    }
}
