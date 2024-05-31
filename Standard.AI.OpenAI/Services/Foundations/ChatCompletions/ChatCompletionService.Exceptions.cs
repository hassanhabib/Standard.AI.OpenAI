// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions;
using Xeptions;

namespace Standard.AI.OpenAI.Services.Foundations.ChatCompletions
{
    internal partial class ChatCompletionService
    {
        private delegate ValueTask<ChatCompletion> ReturningChatCompletionFunction();

        private async ValueTask<ChatCompletion> TryCatch(ReturningChatCompletionFunction returningChatCompletionFunction)
        {
            try
            {
                return await returningChatCompletionFunction();
            }
            catch (NullChatCompletionException nullChatCompletionException)
            {
                throw new ChatCompletionValidationException(nullChatCompletionException);
            }
            catch (InvalidChatCompletionException invalidChatCompletionException)
            {
                throw new ChatCompletionValidationException(invalidChatCompletionException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var invalidConfigurationChatCompletionException =
                    new InvalidConfigurationChatCompletionException(httpResponseUrlNotFoundException);

                throw createChatCompletionDependencyException(
                    invalidConfigurationChatCompletionException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedChatCompletionException =
                    new UnauthorizedChatCompletionException(httpResponseUnauthorizedException);

                throw createChatCompletionDependencyException(
                    unauthorizedChatCompletionException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedChatCompletionException =
                    new UnauthorizedChatCompletionException(httpResponseForbiddenException);

                throw createChatCompletionDependencyException(
                    unauthorizedChatCompletionException);
            }
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundChatCompletionException =
                    new NotFoundChatCompletionException(httpResponseNotFoundException);

                throw new ChatCompletionDependencyValidationException(notFoundChatCompletionException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidChatCompletionException =
                    new InvalidChatCompletionException(httpResponseBadRequestException);

                throw new ChatCompletionDependencyValidationException(invalidChatCompletionException);
            }
            catch (HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallChatCompletionException =
                    new ExcessiveCallChatCompletionException(httpResponseTooManyRequestsException);

                throw new ChatCompletionDependencyValidationException(excessiveCallChatCompletionException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedServerChatCompletionException =
                    new FailedServerChatCompletionException(httpResponseException);

                throw createChatCompletionDependencyException(
                    failedServerChatCompletionException);
            }
            catch (Exception exception)
            {
                var failedChatCompletionServiceException =
                    new FailedChatCompletionServiceException(exception);

                throw new ChatCompletionServiceException(
                    failedChatCompletionServiceException);
            }
        }

        private static ChatCompletionDependencyException createChatCompletionDependencyException(Xeption innerException)
        {
            return new ChatCompletionDependencyException(
                message: "Chat completion dependency error occurred, contact support.",
                innerException);
        }
    }
}