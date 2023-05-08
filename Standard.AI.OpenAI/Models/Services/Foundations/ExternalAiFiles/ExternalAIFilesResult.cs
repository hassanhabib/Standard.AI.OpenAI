// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalAIFiles
{
    internal class ExternalAIFilesResult
    {
        [JsonProperty(propertyName: "object")]
        public string Object { get; set; }

        [JsonProperty(propertyName: "data")]
        public ExternalAIFileResponse[] Files { get; set; }
    }
}
