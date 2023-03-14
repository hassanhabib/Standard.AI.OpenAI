// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenAI.NET.Models.ExternalCompletions
{
    internal class CompletionRequest
    {
        [JsonPropertyName(name: "model")]
        public string Model { get; set; }

        [JsonPropertyName(name: "prompt")]
        public string[] Prompt { get; set; } = new string[0];

        [JsonPropertyName(name: "suffix")]
        public string? Suffix { get; set; }

        [JsonPropertyName(name: "max_tokens")]
        public int? MaxTokens { get; set; }

        [JsonPropertyName(name: "temperature")]
        public double? Temperature { get; set; }

        [JsonPropertyName(name: "top_p")]
        public double? ProbabilityMass { get; set; }

        [JsonPropertyName(name: "n")]
        public int? CompletionsPerPrompt { get; set; }

        [JsonPropertyName(name: "stream")]
        public bool? Stream { get; set; }

        [JsonPropertyName(name: "logprobs")]
        public int? LogProbabilities { get; set; }

        [JsonPropertyName(name: "echo")]
        public bool? Echo { get; set; }

        [JsonPropertyName(name: "stop")]
        public string[]? Stop { get; set; }

        [JsonPropertyName(name: "presence_penalty")]
        public double? PresencePenalty { get; set; }

        [JsonPropertyName(name: "frequency_penalty")]
        public double? FrequencyPenalty { get; set; }

        [JsonPropertyName(name: "best_of")]
        public int? BestOf { get; set; }

        [JsonPropertyName(name: "logit_bias")]
        public Dictionary<string, int>? LogitBias { get; set; }

        [JsonPropertyName(name: "user")]
        public string? User { get; set; }
    }
}
