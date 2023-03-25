// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions;

namespace Standard.AI.OpenAI.Services.Foundations.Completions
{
    internal interface ICompletionService
    {
        ValueTask<Completion> PromptCompletionAsync(Completion completion);
    }
}
