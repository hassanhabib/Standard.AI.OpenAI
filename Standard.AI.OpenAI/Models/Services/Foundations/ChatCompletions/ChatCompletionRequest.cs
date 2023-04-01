// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions
{
    public class ChatCompletionRequest
    {
        public string Model { get; set; }

        public ChatCompletionMessage[] Messages { get; set; } =
            Array.Empty<ChatCompletionMessage>();

        public double Temperature { get; set; } = 1;

        public double ProbabilityMass { get; set; } = 1;

        public int CompletionsPerPrompt { get; set; } = 1;

        public bool Stream { get; set; } = false;

        public string[] Stop { get; set; } = null;

        public int MaxTokens { get; set; } = 16;

        public double PresencePenalty { get; set; } = 0;

        public double FrequencyPenalty { get; set; } = 0;

        public Dictionary<string, int> LogitBias { get; set; } =
            new Dictionary<string, int>();

        public string User { get; set; } = string.Empty;
    }
}
