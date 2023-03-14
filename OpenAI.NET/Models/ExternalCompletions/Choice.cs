// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Text.Json.Serialization;

namespace OpenAI.NET.Models.ExternalCompletions
{
    public class Choice
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("logprobs")]
        public object LogProbabilities { get; set; }

        [JsonPropertyName("finish_reason")]
        public string Finish_reason { get; set; }
    }
}
