// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalCompletions
{
    internal class ExternalCompletionRequest
    {
        [JsonProperty(propertyName: "model")]
        public string Model { get; set; }

        [JsonProperty(propertyName: "prompt")]
        public string[] Prompts { get; set; } = new string[0];

        [JsonProperty(propertyName: "suffix")]
        public string Suffix { get; set; } = null;

        [JsonProperty(propertyName: "max_tokens")]
        public int MaxTokens { get; set; } = 16;

        [JsonProperty(propertyName: "temperature")]
        public double Temperature { get; set; } = 1;

        [JsonProperty(propertyName: "top_p")]
        public double ProbabilityMass { get; set; } = 1;

        [JsonProperty(propertyName: "n")]
        public int CompletionsPerPrompt { get; set; } = 1;

        [JsonProperty(propertyName: "stream")]
        public bool Stream { get; set; } = false;

        [JsonProperty(propertyName: "logprobs")]
        public int? LogProbabilities { get; set; } = null;

        [JsonProperty(propertyName: "echo")]
        public bool Echo { get; set; } = false;

        [JsonProperty(propertyName: "stop")]
        public string[] Stop { get; set; } = null;

        [JsonProperty(propertyName: "presence_penalty")]
        public double PresencePenalty { get; set; } = 0;

        [JsonProperty(propertyName: "frequency_penalty")]
        public double FrequencyPenalty { get; set; } = 0;

        [JsonProperty(propertyName: "best_of")]
        public int BestOf { get; set; } = 1;

        [JsonProperty(propertyName: "logit_bias")]
        public Dictionary<string, int> LogitBias { get; set; } =
            new Dictionary<string, int>();

        [JsonProperty(propertyName: "user")]
        public string User { get; set; } = string.Empty;
    }
}
