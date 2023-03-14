// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace OpenAI.NET.Models.ExternalCompletions
{
    internal class CompletionRequest
    {
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("prompt")]
        public string[] Prompt { get; set; } = new string[0];

        [JsonProperty("suffix")]
        public string? Suffix { get; set; }

        [JsonProperty("max_tokens")]
        public int? MaxTokens { get; set; }

        [JsonProperty("temperature")]
        public double? Temperature { get; set; }

        [JsonProperty("top_p")]
        public double? ProbabilityMass { get; set; }

        [JsonProperty("n")]
        public int? CompletionsPerPrompt { get; set; }

        [JsonProperty("stream")]
        public bool? Stream { get; set; }

        [JsonProperty("logprobs")]
        public int? LogProbabilities { get; set; }

        [JsonProperty("echo")]
        public bool? Echo { get; set; }

        [JsonProperty("stop")]
        public string[]? Stop { get; set; }

        [JsonProperty("presence_penalty")]
        public double? PresencePenalty { get; set; }

        [JsonProperty("frequency_penalty")]
        public double? FrequencyPenalty { get; set; }

        [JsonProperty("best_of")]
        public int? BestOf { get; set; }

        [JsonProperty("logit_bias")]
        public Dictionary<string, int>? LogitBias { get; set; }

        [JsonProperty("user")]
        public string? User { get; set; }
    }
}
