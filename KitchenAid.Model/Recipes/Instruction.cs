using Newtonsoft.Json;

namespace KitchenAid.Model.Recipes
{
    public class Instruction
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("servings")]
        public int Servings { get; set; }
        [JsonProperty("readyInMinutes")]
        public int ReadyInMinutes { get; set; }
        [JsonProperty("instructions")]
        public string Instructions { get; set; }

        public int InstructionId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
