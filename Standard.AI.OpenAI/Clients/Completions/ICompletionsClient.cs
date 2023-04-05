// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Clients.Completions.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions;

namespace Standard.AI.OpenAI.Clients.Completions
{
    public interface ICompletionsClient
    {
        /// <exception cref="CompletionClientValidationException" />
        /// <exception cref="CompletionClientDependencyException" />
        /// <exception cref="CompletionClientServiceException" />
        ValueTask<Completion> PromptCompletionAsync(Completion completion);
    }
}
