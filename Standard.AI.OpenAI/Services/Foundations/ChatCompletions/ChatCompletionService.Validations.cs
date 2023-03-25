// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions;

namespace Standard.AI.OpenAI.Services.Foundations.ChatCompletions
{
    internal partial class ChatCompletionService
    {
        private void ValidateChatCompletionOnSend(ChatCompletion chatCompletion) =>
            ValidateChatCompletionIsNotNull(chatCompletion);

        private void ValidateChatCompletionIsNotNull(ChatCompletion chatCompletion)
        {
            if (chatCompletion is null)
            {
                throw new NullChatCompletionException();
            }
        }
    }
}