// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAIModels;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Acceptance.Clients.AIModels
{
    public partial class AIModelsClientTests
    {
        [Fact]
        public async Task ShouldRetrieveAIModelByNameAsync()
        {
            // given
            string randomString = CreateRandomString();
            string randomAIModelName = randomString;
            string inputAIModelName = randomAIModelName;

            ExternalAIModel randomExternalAIModel =
                CreateRandomExternalAIModel();

            ExternalAIModel retrievedExternalAIModel =
                randomExternalAIModel;

            AIModel expectedAIModel =
                ConvertToAIModel(retrievedExternalAIModel);

            this.wireMockServer.Given(
                Request.Create()
                .UsingGet()
                    .WithPath($"/v1/models/{inputAIModelName}")
                    .WithHeader("Authorization", $"Bearer {this.apiKey}")
                    .WithHeader("OpenAI-Organization", this.organizationId))
                .RespondWith(
                    Response.Create()
                    .WithBodyAsJson(retrievedExternalAIModel));

            // when
            AIModel actualAIModel =
                await this.openAIClient.AIModels.RetrieveAIModelByNameAsync(inputAIModelName);

            // then
            actualAIModel.Should().BeEquivalentTo(expectedAIModel);
        }
    }
}