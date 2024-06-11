// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Clients.ChatCompletions.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions;
using Standard.AI.OpenAI.Services.Foundations.ChatCompletions;
using Xeptions;

namespace Standard.AI.OpenAI.Clients.ChatCompletions
{
    internal class ChatCompletionsClient : IChatCompletionsClient
    {
        private readonly IChatCompletionService chatCompletionService;

        public ChatCompletionsClient(IChatCompletionService chatCompletionService) =>
            this.chatCompletionService = chatCompletionService;

        public async ValueTask<ChatCompletion> SendChatCompletionAsync(ChatCompletion chatCompletion)
        {
            try
            {
                return await this.chatCompletionService.SendChatCompletionAsync(chatCompletion);
            }
            catch (ChatCompletionValidationException completionValidationException)
            {
                throw CreateChatCompletionClientValidationException(
                    completionValidationException.InnerException as Xeption);
            }
            catch (ChatCompletionDependencyValidationException completionDependencyValidationException)
            {
                throw CreateChatCompletionClientValidationException(
                    completionDependencyValidationException.InnerException as Xeption);
            }
            catch (ChatCompletionDependencyException completionDependencyException)
            {
                throw CreateChatCompletionClientDependencyException(
                    completionDependencyException.InnerException as Xeption);
            }
            catch (ChatCompletionServiceException completionServiceException)
            {
                throw CreateChatCompletionClientServiceException(
                    completionServiceException.InnerException as Xeption);
            }
        }

        private static ChatCompletionClientValidationException CreateChatCompletionClientValidationException(
            Xeption innerException)
        {
            return new ChatCompletionClientValidationException(
                message: "Chat completion client validation error occurred, fix errors and try again.",
                innerException);
        }

        private static ChatCompletionClientDependencyException CreateChatCompletionClientDependencyException(
            Xeption innerException)
        {
            return new ChatCompletionClientDependencyException(
                message: "Chat completion dependency error occurred, contact support.",
                innerException);
        }

        private static ChatCompletionClientServiceException CreateChatCompletionClientServiceException(
            Xeption innerException)
        {
            return new ChatCompletionClientServiceException(
                message: "Chat completion client service error occurred, contact support.",
                innerException);
        }
    }
}
