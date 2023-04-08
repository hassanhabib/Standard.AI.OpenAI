// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalFiles
{
    internal class ExternalUploadFileRequest
    {
        [JsonProperty(propertyName: "file")]
        public string File { get; set; }

        [JsonProperty(propertyName: "purpose")]
        public string Purpose { get; set; }
    }
}
