// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalModels
{
    internal class ExternalModelsResult
    {
        [JsonProperty(propertyName: "object")]
        public string Object { get; set; }

        [JsonProperty(propertyName: "data")]
        public ExternalModel[] Data { get; set; }
    }
}