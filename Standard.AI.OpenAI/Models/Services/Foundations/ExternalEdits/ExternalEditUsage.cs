using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalEdits
{
    internal class ExternalEditUsage
    {
        [JsonProperty("prompt_tokens")]
        public int TotalPrompTokens { get; set; }

        [JsonProperty("completion_tokens")]
        public int TotalCompletionTokens { get; set; }

        [JsonProperty("total_tokens")]
        public int TotalTokens { get; set; }
    }
}
