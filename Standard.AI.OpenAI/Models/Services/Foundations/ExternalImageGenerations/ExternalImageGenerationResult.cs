// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalImageGenerations
{
    internal class ExternalImageGenerationResult
    {
        [JsonProperty(propertyName: "url")]
        public string ImageUrl { get; set; }

        [JsonProperty(propertyName: "b64_json")]
        public string Base64EncodedJsonImage { get; set; }
    }
}