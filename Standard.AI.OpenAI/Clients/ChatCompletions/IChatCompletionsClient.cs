// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Clients.ChatCompletions.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;

namespace Standard.AI.OpenAI.Clients.ChatCompletions
{
    public interface IChatCompletionsClient
    {
        /// <exception cref="ChatCompletionClientValidationException" />
        /// <exception cref="ChatCompletionClientDependencyException" />
        /// <exception cref="ChatCompletionClientServiceException" />
        ValueTask<ChatCompletion> SendChatCompletionAsync(ChatCompletion chatCompletion);
    }
}
