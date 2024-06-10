// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions;
using Xeptions;

namespace Standard.AI.OpenAI.Services.Foundations.Completions
{
    internal partial class CompletionService
    {
        private delegate ValueTask<Completion> ReturningCompletionFunction();

        private static async ValueTask<Completion> TryCatch(ReturningCompletionFunction returningCompletionFunction)
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
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var invalidConfigurationCompletionException =
                    new InvalidConfigurationCompletionException(httpResponseUrlNotFoundException);

                throw CreateCompletionDependencyException(
                    invalidConfigurationCompletionException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedCompletionException =
                    new UnauthorizedCompletionException(httpResponseUnauthorizedException);

                throw CreateCompletionDependencyException(
                    unauthorizedCompletionException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedCompletionException =
                    new UnauthorizedCompletionException(httpResponseForbiddenException);

                throw CreateCompletionDependencyException(
                    unauthorizedCompletionException);
            }
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundCompletionException =
                    new NotFoundCompletionException(httpResponseNotFoundException);

                throw CreateCompletionDependencyValidationException(
                    notFoundCompletionException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidCompletionException =
                    new InvalidCompletionException(httpResponseBadRequestException);

                throw CreateCompletionDependencyValidationException( 
                    invalidCompletionException);
            }

            catch (HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallCompletionException =
                    new ExcessiveCallCompletionException(httpResponseTooManyRequestsException);

                throw CreateCompletionDependencyValidationException(
                    excessiveCallCompletionException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedServerCompletionException =
                    new FailedServerCompletionException(httpResponseException);

                throw CreateCompletionDependencyException(
                    failedServerCompletionException);
            }
            catch (Exception exception)
            {
                var failedCompletionServiceException =
                    new FailedCompletionServiceException(exception);

                throw new CompletionServiceException(failedCompletionServiceException);
            }
        }

        private static CompletionDependencyException CreateCompletionDependencyException(Xeption innerException)
        {
            return new CompletionDependencyException(
                message: "Completion dependency error occurred, contact support.",
                innerException);
        }
        

        private static CompletionDependencyValidationException CreateCompletionDependencyValidationException(Xeption innerException)
        {
            return new CompletionDependencyValidationException(
                message: "Completion dependency validation error occurred, fix errors and try again.", 
                innerException);
        }






    }
}
