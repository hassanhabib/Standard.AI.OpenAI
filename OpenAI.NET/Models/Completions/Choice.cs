// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

namespace OpenAI.NET.Models.Completions
{
    public class Choice
    {
        public string Text { get; set; }
        public int Index { get; set; }
        public object LogProbabilities { get; set; }
        public string FinishReason { get; set; }
    }
}
