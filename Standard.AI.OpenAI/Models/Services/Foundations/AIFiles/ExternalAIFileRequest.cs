// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.IO;
using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIFiles
{
    internal class ExternalAIFileRequest
    {
        [JsonProperty("file")]
        public Stream File { get; set; }

        [JsonProperty("purpose")]
        public string Purpose { get; set; }
    }
}
