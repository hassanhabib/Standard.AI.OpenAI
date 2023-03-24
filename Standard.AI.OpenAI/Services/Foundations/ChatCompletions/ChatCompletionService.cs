// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalChatCompletions;

namespace Standard.AI.OpenAI.Services.Foundations.ChatCompletions
{
    internal class ChatCompletionService : IChatCompletionService
    {
        private readonly IOpenAIBroker openAIBroker;

        public ChatCompletionService(IOpenAIBroker openAIBroker) =>
            this.openAIBroker = openAIBroker;

        public async ValueTask<ChatCompletion> SendChatCompletionAsync(ChatCompletion chatCompletion)
        {
            ExternalChatCompletionRequest externalChatCompletionRequest =
                ConvertToChatCompletionRequest(chatCompletion);

            ExternalChatCompletionResponse externalChatCompletionResponse =
                await this.openAIBroker.PostChatCompletionRequestAsync(externalChatCompletionRequest);

            return ConvertToChatCompletion(chatCompletion, externalChatCompletionResponse);
        }

        private static ExternalChatCompletionRequest ConvertToChatCompletionRequest(ChatCompletion chatCompletion)
        {
            return new ExternalChatCompletionRequest
            {
                Model = chatCompletion.Request.Model,

                Messages = chatCompletion.Request.Messages.Select(message =>
                {
                    return new ExternalChatCompletionMessage
                    {
                        Role = message.Role,
                        Content = message.Content
                    };
                }).ToArray(),

                Temperature = chatCompletion.Request.Temperature,
                ProbabilityMass = chatCompletion.Request.ProbabilityMass,
                CompletionsPerPrompt = chatCompletion.Request.CompletionsPerPrompt,
                Stream = chatCompletion.Request.Stream,
                Stop = chatCompletion.Request.Stop,
                MaxTokens = chatCompletion.Request.MaxTokens,
                PresencePenalty = chatCompletion.Request.PresencePenalty,
                FrequencyPenalty = chatCompletion.Request.FrequencyPenalty,
                LogitBias = chatCompletion.Request.LogitBias,
                User = chatCompletion.Request.User
            };
        }

        private static ChatCompletion ConvertToChatCompletion(
            ChatCompletion chatCompletion,
            ExternalChatCompletionResponse externalChatCompletionResponse)
        {

            chatCompletion.Response = new ChatCompletionResponse
            {
                Id = externalChatCompletionResponse.Id,
                Created = externalChatCompletionResponse.Created,
                Choices = externalChatCompletionResponse.Choices.Select(choice =>
                {
                    return new ChatCompletionChoice
                    {
                        FinishReason = choice.FinishReason,
                        Index = choice.Index,

                        Message = new ChatCompletionMessage
                        {
                            Content = choice.Message.Content,
                            Role = choice.Message.Role
                        }
                    };

                }).ToArray(),
                Usage = new ChatCompletionUsage
                {
                    CompletionTokens = externalChatCompletionResponse.Usage.CompletionTokens,
                    PromptTokens = externalChatCompletionResponse.Usage.PromptTokens,
                    TotalTokens = externalChatCompletionResponse.Usage.TotalTokens
                },
                Object = externalChatCompletionResponse.Object
            };

            return chatCompletion;
        }
    }
}
