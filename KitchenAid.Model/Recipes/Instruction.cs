using Newtonsoft.Json;

namespace KitchenAid.Model.Recipes
{
    /// <summary>
    /// Model for a cooking instruction for a recipee
    /// Most attributes in this class are only to manage to deserialize the
    /// data from spoonacular api.
    /// </summary>
    public class Instruction
    {
        /// <summary>Gets or sets the servings.</summary>
        /// <value>The servings.</value>
        [JsonProperty("servings")]
        public int Servings { get; set; }
        /// <summary>Gets or sets the ready in minutes.</summary>
        /// <value>The ready in minutes.</value>
        [JsonProperty("readyInMinutes")]
        public int ReadyInMinutes { get; set; }
        /// <summary>Gets or sets the instructions.</summary>
        /// <value>The instructions.</value>
        [JsonProperty("instructions")]
        public string Instructions { get; set; }

        /// <summary>Gets or sets the instruction identifier.</summary>
        /// <value>The instruction identifier.</value>
        public int InstructionId { get; set; }
        /// <summary>Gets or sets the recipe identifier.</summary>
        /// <value>The recipe identifier.</value>
        public int RecipeId { get; set; }
    }
}
