// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalChatCompletions
{
    internal class ExternalChatCompletionChoice
    {
        [JsonProperty(propertyName: "index")]
        public int Index { get; set; }

        [JsonProperty(propertyName: "message")]
        public ExternalChatCompletionMessage Message { get; set; }

        [JsonProperty(propertyName: "finish_reason")]
        public string FinishReason { get; set; }
    }
}
