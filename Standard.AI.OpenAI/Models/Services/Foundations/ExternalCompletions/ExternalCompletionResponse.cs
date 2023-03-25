// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalCompletions
{
    internal class ExternalCompletionResponse
    {
        [JsonProperty(propertyName: "id")]
        public string Id { get; set; }

        [JsonProperty(propertyName: "_object")]
        public string Object { get; set; }

        [JsonProperty(propertyName: "created")]
        public int Created { get; set; }

        [JsonProperty(propertyName: "model")]
        public string Model { get; set; }

        [JsonProperty(propertyName: "choices")]
        public ExternalCompletionChoice[] Choices { get; set; }

        [JsonProperty(propertyName: "usage")]
        public ExternalCompletionUsage Usage { get; set; }
    }
}
