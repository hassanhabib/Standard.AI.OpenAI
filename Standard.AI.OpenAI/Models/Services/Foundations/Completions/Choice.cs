// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

namespace Standard.AI.OpenAI.Models.Services.Foundations.Completions
{
    public class Choice
    {
        public string Text { get; set; }
        public int Index { get; set; }
        public object LogProbabilities { get; set; }
        public string FinishReason { get; set; }
    }
}
