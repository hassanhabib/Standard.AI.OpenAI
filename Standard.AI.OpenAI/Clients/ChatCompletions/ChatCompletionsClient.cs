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
                throw new ChatCompletionClientValidationException(
                    completionValidationException.InnerException as Xeption);
            }
            catch (ChatCompletionDependencyValidationException completionDependencyValidationException)
            {
                throw new ChatCompletionClientValidationException(
                    completionDependencyValidationException.InnerException as Xeption);
            }
            catch (ChatCompletionDependencyException completionDependencyException)
            {
                throw new ChatCompletionClientDependencyException(
                    completionDependencyException.InnerException as Xeption);
            }
            catch (ChatCompletionServiceException completionServiceException)
            {
                throw new ChatCompletionClientServiceException(
                    completionServiceException.InnerException as Xeption);
            }
        }
    }
}
