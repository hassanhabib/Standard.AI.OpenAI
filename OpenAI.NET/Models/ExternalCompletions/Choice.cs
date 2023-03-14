// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Newtonsoft.Json;

namespace OpenAI.NET.Models.ExternalCompletions
{
    public class Choice
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("logprobs")]
        public object LogProbabilities { get; set; }

        [JsonProperty("finish_reason")]
        public string Finish_reason { get; set; }
    }
}
