// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalImageGenerations
{
    internal class ExternalImageGenerationResponse
    {
        [JsonProperty(propertyName: "created")]
        public int Created { get; set; }

        [JsonProperty(propertyName: "data")]
        public ExternalImageGenerationResult[] Results { get; set; }
    }
}