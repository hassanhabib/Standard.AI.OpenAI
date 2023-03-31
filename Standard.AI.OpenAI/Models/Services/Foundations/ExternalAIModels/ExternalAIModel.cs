// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalAIModels
{
    internal class ExternalAIModel
    {
        [JsonProperty(propertyName: "id")]
        public string Id { get; set; }

        [JsonProperty(propertyName: "object")]
        public string Object { get; set; }

        [JsonProperty(propertyName: "created")]
        public int Created { get; set; }

        [JsonProperty(propertyName: "owned_by")]
        public string OwnedBy { get; set; }

        [JsonProperty(propertyName: "permission")]
        public ExternalAIModelPermission[] Permissions { get; set; }

        [JsonProperty(propertyName: "root")]
        public string Root { get; set; }

        [JsonProperty(propertyName: "parent")]
        public string Parent { get; set; }
    }
}
