// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalChatCompletions;

namespace Standard.AI.OpenAI.Brokers.OpenAIs
{
    internal partial class OpenAIBroker
    {
        public async ValueTask<ExternalChatCompletionResponse> PostChatCompletionAsync(
            ExternalChatCompletionRequest chatCompletionRequest)
        {
            return await PostAsync<ExternalChatCompletionRequest, ExternalChatCompletionResponse>(
                relativeUrl: "v1/chat/completions",
                content: chatCompletionRequest);
        }
    }
}
