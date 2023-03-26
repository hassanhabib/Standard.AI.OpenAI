// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions;

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

                throw new ChatCompletionDependencyException(invalidConfigurationChatCompletionException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidChatCompletionException =
                    new InvalidChatCompletionException(httpResponseBadRequestException);

                throw new ChatCompletionDependencyValidationException(invalidChatCompletionException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedCompletionException =
                    new UnauthorizedChatCompletionException(httpResponseUnauthorizedException);

                throw new ChatCompletionDependencyException(unauthorizedCompletionException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedCompletionException =
                    new UnauthorizedChatCompletionException(httpResponseForbiddenException);

                throw new ChatCompletionDependencyException(unauthorizedCompletionException);
            }
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundChatCompletionException =
                    new NotFoundChatCompletionException(httpResponseNotFoundException);

                throw new ChatCompletionDependencyValidationException(notFoundChatCompletionException);
            }
            catch (HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallChatCompletionException =
                    new ExcessiveCallChatCompletionException(httpResponseTooManyRequestsException);

                throw new ChatCompletionDependencyValidationException(excessiveCallChatCompletionException);
            }
        }
    }
}