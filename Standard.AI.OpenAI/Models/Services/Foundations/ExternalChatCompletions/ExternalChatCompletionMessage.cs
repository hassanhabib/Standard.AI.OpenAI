// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalChatCompletions
{
    internal class ExternalChatCompletionMessage
    {
        [JsonProperty(propertyName: "role")]
        public string Role { get; set; }

        [JsonProperty(propertyName: "content")]
        public string Content { get; set; }
    }
}
