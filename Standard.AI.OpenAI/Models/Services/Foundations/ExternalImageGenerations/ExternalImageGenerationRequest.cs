// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalImageGenerations
{
    internal class ExternalImageGenerationRequest
    {
        [JsonProperty(propertyName: "prompt")]
        public string Prompt { get; set; }

        [JsonProperty(propertyName: "n")]
        public int ImagesToGenerate { get; set; }

        [JsonProperty(propertyName: "size")]
        public string ImageSize { get; set; }

        [JsonProperty(propertyName: "response_format")]
        public string ResponseFormat { get; set; }

        [JsonProperty(propertyName: "user")]
        public string User { get; set; }
    }
}