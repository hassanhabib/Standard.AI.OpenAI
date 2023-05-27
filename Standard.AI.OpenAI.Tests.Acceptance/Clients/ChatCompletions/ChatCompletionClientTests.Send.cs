// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Newtonsoft.Json;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalChatCompletions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Acceptance.Clients.ChatCompletions
{
    public partial class ChatCompletionClientTests
    {
        [Fact]
        public async Task ShouldSendChatCompletionAsync()
        {
            // given
            ChatCompletion randomChatCompletion = CreateRandomChatCompletion();
            ChatCompletion inputChatCompletion = randomChatCompletion;

            ExternalChatCompletionRequest chatCompletionRequest =
                ConvertToChatCompletionRequest(inputChatCompletion);

            ExternalChatCompletionResponse chatCompletionResponse =
                CreateRandomExternalChatCompletionResponse();

            ChatCompletion expectedChatCompletion =
                inputChatCompletion.DeepClone();

            expectedChatCompletion =
                ConvertToChatCompletion(expectedChatCompletion, chatCompletionResponse);

            var jsonSerializationSettings = new JsonSerializerSettings();
            jsonSerializationSettings.DefaultValueHandling = DefaultValueHandling.Ignore;

            this.wireMockServer.Given(
                Request.Create()
                .UsingPost()
                    .WithPath("/v1/chat/completions")
                    .WithHeader("Authorization", $"Bearer {this.apiKey}")
                    .WithHeader("OpenAI-Organization", this.organizationId)
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithBody(JsonConvert.SerializeObject(
                        chatCompletionRequest,
                        jsonSerializationSettings)))
                .RespondWith(
                    Response.Create()
                    .WithBodyAsJson(chatCompletionResponse));

            // when
            ChatCompletion actualChatCompletion =
                await this.openAIClient.ChatCompletions.SendChatCompletionAsync(
                    inputChatCompletion);

            // then
            actualChatCompletion.Should().BeEquivalentTo(expectedChatCompletion);
        }
    }
}
