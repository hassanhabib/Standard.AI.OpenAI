// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

namespace Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions
{
    public class ChatCompletionChoice
    {
        public int Index { get; set; }

        public ChatCompletionMessage Message { get; set; }

        public string FinishReason { get; set; }
    }
}
