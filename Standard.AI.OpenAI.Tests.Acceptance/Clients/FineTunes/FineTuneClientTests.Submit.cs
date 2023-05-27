// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Newtonsoft.Json;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalFineTunes;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Acceptance.Clients.FineTunes
{
    public partial class FineTuneClientTests
    {
        [Fact]
        public async Task ShouldSubmitFineTuneAsync()
        {
            // given
            FineTune randomFineTune = CreateRandomFineTune();
            FineTune inputFineTune = randomFineTune;
            ExternalFineTuneRequest externalFineTuneRequest = ConvertToFineTuneRequest(inputFineTune);
            ExternalFineTuneResponse externalFineTuneResponse = CreateRandomExternalFineTuneResponse();
            FineTune expectedFineTune = inputFineTune.DeepClone();
            expectedFineTune = ConvertToFineTune(expectedFineTune, externalFineTuneResponse);

            var jsonSerializationSettings = new JsonSerializerSettings();
            jsonSerializationSettings.DefaultValueHandling = DefaultValueHandling.Ignore;

            this.wireMockServer.Given(
                Request.Create()
                    .UsingPost()
                        .WithPath("/v1/fine-tunes")
                        .WithHeader("Authorization", $"Bearer {this.apiKey}")
                        .WithHeader("OpenAI-Organization", $"{this.organizationId}")
                        .WithHeader("Content-Type", "application/json; charset=utf-8")
                        .WithBody(JsonConvert.SerializeObject(
                            externalFineTuneRequest,
                            jsonSerializationSettings)))
                    .RespondWith(
                        Response.Create()
                            .WithBodyAsJson(externalFineTuneResponse));

            // when
            FineTune actualFineTune =
                await this.openAIClient.FineTuneClient.SubmitFineTuneAsync(
                    inputFineTune);

            // then
            actualFineTune.Should().BeEquivalentTo(expectedFineTune);
        }
    }
}
