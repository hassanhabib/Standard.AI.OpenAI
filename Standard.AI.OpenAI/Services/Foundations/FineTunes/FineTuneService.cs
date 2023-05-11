// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes;

namespace Standard.AI.OpenAI.Services.Foundations.FineTunes
{
    internal partial class FineTuneService : IFineTuneService
    {
        private readonly IOpenAIBroker openAIBroker;

        public FineTuneService(IOpenAIBroker openAIBroker)
        {
            this.openAIBroker = openAIBroker;
        }

        public ValueTask<FineTune> SubmitFineTuneAsync(FineTune fineTune)
        {
            throw new NotImplementedException();
        }
    }
}
