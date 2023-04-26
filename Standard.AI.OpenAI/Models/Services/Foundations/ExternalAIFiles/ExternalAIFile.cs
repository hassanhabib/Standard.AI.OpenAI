// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalFiles
{
    public class ExternalAIFiles
    {
        [JsonProperty(propertyName: "id")]
        Guid Id { get; set; }
        [JsonProperty(propertyName: "object")]
        string Object { get; set; }
        [JsonProperty(propertyName: "bytes")]
        int Bytes { get; set; }
        [JsonProperty(propertyName: "created_at")]
        int CreatedDate { get; set; }
        [JsonProperty(propertyName: "fileName")]
        string FileName { get; set; }
        [JsonProperty(propertyName: "purpose")]
        string Purpose { get; set; }
    }
}