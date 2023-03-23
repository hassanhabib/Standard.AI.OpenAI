// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalModels
{
    internal class ExternalModel
    {
        [JsonProperty(propertyName: "id")]
        public string Id { get; set; }

        [JsonProperty(propertyName: "_object")]
        public string _object { get; set; }

        [JsonProperty(propertyName: "created")]
        public int Created { get; set; }

        [JsonProperty(propertyName: "owned_by")]
        public string Owned_by { get; set; }

        [JsonProperty(propertyName: "permission")]
        public ExternalPermission[] Permission { get; set; }

        [JsonProperty(propertyName: "root")]
        public string Root { get; set; }

        [JsonProperty(propertyName: "parent")]
        public object Parent { get; set; }
    }
}
