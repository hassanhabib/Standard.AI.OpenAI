// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Collections.Generic;

namespace OpenAI.NET.Models.Completions
{
    public class CompletionRequest
    {
        public string Model { get; set; }
        public string[] Prompt { get; set; }
        public string Suffix { get; set; }
        public int MaxTokens { get; set; }
        public double Temperature { get; set; }
        public double ProbabilityMass { get; set; }
        public int CompletionsPerPrompt { get; set; }
        public bool Stream { get; set; } = false;
        public int? LogProbabilities { get; set; }
        public bool Echo { get; set; } = false;
        public string[] Stop { get; set; }
        public double PresencePenalty { get; set; }
        public double FrequencyPenalty { get; set; }
        public int BestOf { get; set; }
        public Dictionary<string, int> LogitBias { get; set; }
        public string User { get; set; }
    }
}
