// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI.NET.Models.Completions;

namespace OpenAI.NET.Clients.Completions
{
    public interface ICompletionsClient
    {
        ValueTask<Completion> PromptCompletionAsync(Completion completion);
    }
}
