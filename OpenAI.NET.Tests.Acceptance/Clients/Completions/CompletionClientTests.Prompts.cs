// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Newtonsoft.Json;
using OpenAI.NET.Models.Services.Foundations.Completions;
using OpenAI.NET.Models.Services.Foundations.ExternalCompletions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace OpenAI.NET.Tests.Acceptance.Clients.Completions
{
    public partial class CompletionClientTests
    {
        [Fact]
        public async Task ShouldPromptCompletionAsync()
        {
            // given
            Completion randomCompletion = CreateRandomCompletion();
            Completion inputCompletion = randomCompletion;

            ExternalCompletionRequest completionRequest =
                ConvertToCompletionRequest(inputCompletion);

            ExternalCompletionResponse completionResponse =
                CreateRandomExternalCompletionResponse();

            Completion expectedCompletion = inputCompletion.DeepClone();
            expectedCompletion = ConvertToCompletion(inputCompletion, completionResponse);

            var jsonSerializationSettings = new JsonSerializerSettings();
            jsonSerializationSettings.DefaultValueHandling = DefaultValueHandling.Ignore;

            this.wireMockServer.Given(
                Request.Create()
                .WithPath("/v1/completions")
                .WithHeader("Authorization", $"Bearer {this.apiKey}")
                .WithHeader("OpenAI-Organization", $"{this.organizationId}")
                .WithBody(JsonConvert.SerializeObject(
                    completionRequest,
                    jsonSerializationSettings)))
                .RespondWith(
                    Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBodyAsJson(completionResponse));

            // when
            Completion actualCompletion =
                await this.openAIClient.Completions.PromptCompletionAsync(
                    inputCompletion);

            // then
            actualCompletion.Should().BeEquivalentTo(expectedCompletion);
        }
    }
}
