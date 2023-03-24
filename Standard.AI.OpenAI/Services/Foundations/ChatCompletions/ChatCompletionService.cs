// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;

namespace Standard.AI.OpenAI.Services.Foundations.ChatCompletions
{
    internal class ChatCompletionService : IChatCompletionService
    {
        private readonly IOpenAIBroker openAIBroker;

        public ChatCompletionService(IOpenAIBroker openAIBroker) =>
            this.openAIBroker = openAIBroker;

        public ValueTask<ChatCompletion> SendChatCompletionAsync(ChatCompletion chatCompletion)
        {
            throw new NotImplementedException();
        }
    }
}
