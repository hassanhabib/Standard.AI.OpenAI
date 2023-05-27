// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
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
        public async Task ShouldRetrieveAllAIModelsAsync()
        {
            // given
            ExternalAIModelsResult randomExternalAIModelResult =
                CreateRandomExternalAIModelsResult();

            ExternalAIModelsResult retrievedAIModelResult =
                randomExternalAIModelResult;

            IEnumerable<AIModel> expectedAIModels =
                ConvertToAIModels(retrievedAIModelResult);

            this.wireMockServer.Given(
                Request.Create()
                .UsingGet()
                    .WithPath("/v1/models")
                    .WithHeader("Authorization", $"Bearer {this.apiKey}")
                    .WithHeader("OpenAI-Organization", this.organizationId))
                .RespondWith(
                    Response.Create()
                    .WithBodyAsJson(retrievedAIModelResult));

            // when
            IEnumerable<AIModel> actualAIModels =
                await this.openAIClient.AIModels.RetrieveAIModelsAsync();

            // then
            actualAIModels.Should().BeEquivalentTo(expectedAIModels);
        }
    }
}
