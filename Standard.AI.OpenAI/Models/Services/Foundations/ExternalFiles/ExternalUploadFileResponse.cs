// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalFiles
{
    internal class ExternalUploadFileResponse
    {
        [JsonProperty(propertyName: "id")]
        public string Id { get; set; }

        [JsonProperty(propertyName: "object")]
        public string Object { get; set; }

        [JsonProperty(propertyName: "bytes")]
        public int Bytes { get; set; }

        [JsonProperty(propertyName: "created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty(propertyName: "filename")]
        public string FileName { get; set; }

        [JsonProperty(propertyName: "purpose")]
        public string Purpose { get; set; }
    }
}
