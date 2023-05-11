// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalFineTunes;

namespace Standard.AI.OpenAI.Brokers.OpenAIs
{
    internal partial class OpenAIBroker
    {
        public ValueTask<ExternalFineTuneResponse> PostFineTuneAsync(
            ExternalFineTuneRequest externalFineTuneRequest)
        {
            return PostAsync<ExternalFineTuneRequest, ExternalFineTuneResponse>(
                relativeUrl: "v1/fine-tunes",
                content: externalFineTuneRequest);
        }
    }
}
