// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace OpenAI.NET.Models.ExternalCompletions
{
    public class CompletionResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("_object")]
        public string Object { get; set; }

        [JsonProperty("created")]
        public int Created { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("choices")]
        public Choice[] Choices { get; set; }

        [JsonProperty("usage")]
        public Usage Usage { get; set; }
    }
}
