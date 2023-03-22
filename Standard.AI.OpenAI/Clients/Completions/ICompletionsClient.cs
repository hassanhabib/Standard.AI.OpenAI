// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions;

namespace Standard.AI.OpenAI.Clients.Completions
{
    public interface ICompletionsClient
    {
        ValueTask<Completion> PromptCompletionAsync(Completion completion);
    }
}
