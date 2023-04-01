// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalEdits
{
    internal partial class ExternalEditResponse
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("created")]
        public int Created { get; set; }

        [JsonProperty("choices")]
        public ExternalEditChoice[] Choices { get; set; }

        [JsonProperty("usage")]
        public ExternalEditUsage Usage { get; set; }
    }
}
