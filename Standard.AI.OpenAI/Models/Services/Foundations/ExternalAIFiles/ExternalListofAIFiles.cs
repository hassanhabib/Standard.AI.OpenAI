// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using Newtonsoft.Json;
using Standard.AI.OpenAI.Models.Services.Foundations.Files;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalFiles
{
    internal class ExternalListofAIFiles
    {
        [JsonProperty(propertyName: "data")]
        public List<File> ListFiles { get; set; }

        [JsonProperty(propertyName: "object")]
        public string Object {get;set;}
    }
}