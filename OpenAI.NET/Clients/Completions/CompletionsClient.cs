// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using OpenAI.NET.Models.Clients.Completions.Exceptions;
using OpenAI.NET.Models.Completions;
using OpenAI.NET.Models.Completions.Exceptions;
using OpenAI.NET.Services.Foundations.Completions;
using Xeptions;

namespace OpenAI.NET.Clients.Completions
{
    internal class CompletionsClient : ICompletionClient
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
                throw new CompletionClientValidationException(
                    completionValidationException.InnerException as Xeption);
            }
            catch (CompletionDependencyValidationException completionDependencyValidationException)
            {
                throw new CompletionClientValidationException(
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
    }
}
