// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using OpenAI.NET.Brokers.OpenAIs;
using OpenAI.NET.Models.Completions;

namespace OpenAI.NET.Services.Foundations.Completions
{
    internal partial class CompletionService : ICompletionService
    {
        private readonly IOpenAIBroker openAiBroker;

        public CompletionService(IOpenAIBroker openAiBroker) =>
            this.openAiBroker = openAiBroker;

        public ValueTask<Completion> PromptCompletionAsync(Completion completion)
        {
            throw new NotImplementedException();
        }
    }
}
