// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalChatCompletions
{
    internal class ExternalChatCompletionResponse
    {
        [JsonProperty(propertyName: "id")]
        public string Id { get; set; }

        [JsonProperty(propertyName: "_object")]
        public string Object { get; set; }

        [JsonProperty(propertyName: "created")]
        public int Created { get; set; }

        [JsonProperty(propertyName: "choices")]
        public ExternalChatCompletionChoice[] Choices { get; set; }

        [JsonProperty(propertyName: "usage")]
        public ExternalChatCompletionUsage Usage { get; set; }
    }
}
