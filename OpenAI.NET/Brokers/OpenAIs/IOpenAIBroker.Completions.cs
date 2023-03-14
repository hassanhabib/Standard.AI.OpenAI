// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI.NET.Models.ExternalCompletions;

namespace OpenAI.NET.Brokers.OpenAIs
{
    internal partial interface IOpenAIBroker
    {
        ValueTask<CompletionResponse> PostCompletionRequestAsync(string completionRequest);
    }
}