// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;

namespace Standard.AI.OpenAI.Brokers.OpenAIs
{
    internal partial class OpenAIBroker
    {
        public ValueTask<ExternalFileResponse> PostFileFormAsync(
            ExternalFileRequest externalFileRequest)
        {
            throw new NotImplementedException();
        }
    }
}
