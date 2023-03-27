// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Newtonsoft.Json;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalCompletions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Acceptance.Clients.Completions
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
            expectedCompletion = ConvertToCompletion(expectedCompletion, completionResponse);

            var jsonSerializationSettings = new JsonSerializerSettings();
            jsonSerializationSettings.DefaultValueHandling = DefaultValueHandling.Ignore;

            this.wireMockServer.Given(
                Request.Create()
                    .WithPath("/v1/completions")
                    .WithHeader("Authorization", $"{JwtBearerDefaults.AuthenticationScheme} {this.apiKey}")
                    .WithHeader("OpenAI-Organization", $"{this.organizationId}")
                    .WithHeader("Content-Type", $"{MediaTypeNames.Application.Json}; charset={Encoding.UTF8.WebName}")
                .WithBody(JsonConvert.SerializeObject(
                    completionRequest,
                    jsonSerializationSettings)))
                .RespondWith(
                    Response.Create()
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
