// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalEdits;

namespace Standard.AI.OpenAI.Brokers.OpenAIs
{
    internal partial class OpenAIBroker
    {
        public async ValueTask<ExternalEditResponse> PostEditRequestAsync(ExternalEditRequest externalEditRequest) =>
            await PostAsync<ExternalEditRequest, ExternalEditResponse>(relativeUrl: "v1/edits", externalEditRequest);
    }
}
