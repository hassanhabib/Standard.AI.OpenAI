// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalCompletions
{
    internal class ExternalCompletionChoice
    {
        [JsonProperty(propertyName: "text")]
        public string Text { get; set; }

        [JsonProperty(propertyName: "index")]
        public int Index { get; set; }

        [JsonProperty(propertyName: "logprobs")]
        public object LogProbabilities { get; set; }

        [JsonProperty(propertyName: "finish_reason")]
        public string FinishReason { get; set; }
    }
}
