// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions;
using System.Threading.Tasks;

namespace Standard.AI.OpenAI.Services.Foundations.ChatCompletions
{
    internal partial class ChatCompletionService
    {
        private delegate ValueTask<ChatCompletion> ReturningChatCompletionFunction();

        private async ValueTask<ChatCompletion> TryCatch(ReturningChatCompletionFunction returningChatCompletionFunction)
        {
            try
            {
                return await returningChatCompletionFunction();
            }
            catch (NullChatCompletionException nullChatCompletionException)
            {
                throw new ChatCompletionValidationException(nullChatCompletionException);
            }
            catch (InvalidChatCompletionException invalidChatCompletionException)
            {
                throw new ChatCompletionValidationException(invalidChatCompletionException);
            }
        }
    }
}