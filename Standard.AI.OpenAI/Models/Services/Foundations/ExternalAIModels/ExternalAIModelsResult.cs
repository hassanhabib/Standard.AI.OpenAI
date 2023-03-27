// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalAIModels
{
    internal class ExternalAIModelsResult
    {
        [JsonProperty(propertyName: "object")]
        public string Object { get; set; }

        [JsonProperty(propertyName: "data")]
        public ExternalAIModel[] AIModels { get; set; }
    }
}
