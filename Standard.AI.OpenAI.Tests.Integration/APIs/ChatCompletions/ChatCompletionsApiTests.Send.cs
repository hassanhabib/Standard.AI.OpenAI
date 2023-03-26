// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Integration.APIs.ChatCompletions
{
    public partial class ChatCompletionsApiTests
    {
        [Fact(Skip = "This test is only for releases")]
        public async Task ShouldSendChatCompletionAsync()
        {
            // given
            var inputChatCompletion = new ChatCompletion
            {
                Request = new ChatCompletionRequest
                {
                    Model = "gpt-3.5-turbo",
                    Messages = new ChatCompletionMessage[]
                    {
                        new ChatCompletionMessage
                        {
                            Role = "user",
                            Content = "Hello!"
                        }
                    }
                }
            };

            // when
            ChatCompletion responseChatCompletion =
                await this.openAIClient.ChatCompletions.SendChatCompletionAsync(
                    inputChatCompletion);

            // then
            Assert.NotNull(responseChatCompletion.Response);
        }
    }
}