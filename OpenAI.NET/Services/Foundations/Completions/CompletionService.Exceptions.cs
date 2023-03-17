// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using OpenAI.NET.Models.Completions;
using OpenAI.NET.Models.Completions.Exceptions;
using RESTFulSense.Exceptions;

namespace OpenAI.NET.Services.Foundations.Completions
{
    internal partial class CompletionService
    {
        private delegate ValueTask<Completion> ReturningCompletionFunction();

        private async ValueTask<Completion> TryCatch(ReturningCompletionFunction returningCompletionFunction)
        {
            try
            {
                return await returningCompletionFunction();
            }
            catch (NullCompletionException nullCompletionException)
            {
                throw new CompletionValidationException(nullCompletionException);
            }
            catch (InvalidCompletionException invalidCompletionException)
            {
                throw new CompletionValidationException(
                    invalidCompletionException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedCompletionException =
                    new UnauthorizedCompletionException(httpResponseUnauthorizedException);

                throw new CompletionDependencyException(unauthorizedCompletionException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedCompletionException =
                    new UnauthorizedCompletionException(httpResponseForbiddenException);

                throw new CompletionDependencyException(unauthorizedCompletionException);
            }
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundCompletionException =
                    new NotFoundCompletionException(httpResponseNotFoundException);

                throw new CompletionDependencyValidationException(notFoundCompletionException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidCompletionException =
                    new InvalidCompletionException(httpResponseBadRequestException);

                throw new CompletionDependencyValidationException(invalidCompletionException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var invalidConfigurationCompletionException =
                    new InvalidConfigurationCompletionException(httpResponseUrlNotFoundException);

                throw new CompletionDependencyException(invalidConfigurationCompletionException);
            }
            catch(HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallCompletionException =
                    new ExcessiveCallCompletionException(httpResponseTooManyRequestsException);

                throw new CompletionDependencyValidationException(excessiveCallCompletionException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedServerCompletionException =
                    new FailedServerCompletionException(
                        httpResponseException);

                throw new CompletionDependencyException(failedServerCompletionException);
            }
        }
    }
}
