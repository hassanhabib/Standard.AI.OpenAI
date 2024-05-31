// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Clients.Completions.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions;
using Standard.AI.OpenAI.Services.Foundations.Completions;
using Xeptions;

namespace Standard.AI.OpenAI.Clients.Completions
{
    internal class CompletionsClient : ICompletionsClient
    {
        private readonly ICompletionService completionService;

        public CompletionsClient(ICompletionService completionService) =>
            this.completionService = completionService;

        public async ValueTask<Completion> PromptCompletionAsync(Completion completion)
        {
            try
            {
                return await this.completionService.PromptCompletionAsync(completion);
            }
            catch (CompletionValidationException completionValidationException)
            {
                throw createCompletionClientValidationException(
                    completionValidationException.InnerException as Xeption);
            }
            catch (CompletionDependencyValidationException completionDependencyValidationException)
            {
                throw createCompletionClientValidationException(
                    completionDependencyValidationException.InnerException as Xeption);
            }
            catch (CompletionDependencyException completionDependencyException)
            {
                throw new CompletionClientDependencyException(
                    completionDependencyException.InnerException as Xeption);
            }
            catch (CompletionServiceException completionServiceException)
            {
                throw new CompletionClientServiceException(
                    completionServiceException.InnerException as Xeption);
            }
        }

        private static CompletionClientValidationException createCompletionClientValidationException(Xeption innerException)
        {
            return new CompletionClientValidationException(
                message: "Completion client validation error occurred, fix errors and try again.",
                innerException);
        }
    }
}
