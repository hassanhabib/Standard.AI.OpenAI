// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using OpenAI.NET.Models.Completions;

namespace OpenAI.NET.Services.Foundations.Completions
{
    internal interface ICompletionService
    {
        ValueTask<Completion> PromptCompletionAsync(Completion completion);
    }
}
