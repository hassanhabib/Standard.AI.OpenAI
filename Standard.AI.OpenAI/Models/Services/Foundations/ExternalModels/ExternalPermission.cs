// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalModels
{
    internal class ExternalPermission
    {
        [JsonProperty(propertyName: "id")]
        public string Id { get; set; }

        [JsonProperty(propertyName: "_object")]
        public string Object { get; set; }

        [JsonProperty(propertyName: "created")]
        public int Created { get; set; }

        [JsonProperty(propertyName: "allow_create_engine")]
        public bool AllowCreateEngine { get; set; }

        [JsonProperty(propertyName: "allow_sampling")]
        public bool AllowSampling { get; set; }

        [JsonProperty(propertyName: "allow_logprobs")]
        public bool AllowLogprobs { get; set; }

        [JsonProperty(propertyName: "allow_search_indices")]
        public bool AllowSearchIndices { get; set; }

        [JsonProperty(propertyName: "allow_view")]
        public bool AllowView { get; set; }

        [JsonProperty(propertyName: "allow_fine_tuning")]
        public bool AllowFineTuning { get; set; }

        [JsonProperty(propertyName: "organization")]
        public string Organization { get; set; }

        [JsonProperty(propertyName: "group")]
        public object Group { get; set; }

        [JsonProperty(propertyName: "is_blocking")]
        public bool IsBlocking { get; set; }
    }
}
