// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAIFiles;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Acceptance.Clients.AIFiles
{
    public partial class AIFilesClientTests
    {
        [Fact]
        public async Task ShouldRetrieveAllFilesAsync()
        {
            // given
            ExternalAIFilesResult randomExternalAIFilesResult =
                CreateRandomExternalAIFilesResult();

            ExternalAIFilesResult retrievedAIFilesResult =
                randomExternalAIFilesResult;

            IEnumerable<AIFileResponse> expectedAIFiles =
                ConvertToAIFiles(retrievedAIFilesResult);

            this.wireMockServer.Given(
                Request.Create()
                .UsingGet()
                    .WithPath("/v1/files")
                    .WithHeader("Authorization", $"Bearer {this.apiKey}")
                    .WithHeader("OpenAI-Organization", this.organizationId))
                .RespondWith(
                    Response.Create()
                    .WithBodyAsJson(retrievedAIFilesResult));

            // when
            IEnumerable<AIFileResponse> actualAIFiles =
                await this.openAIClient.AIFiles.RetrieveAllFilesAsync();

            // then
            actualAIFiles.Should().BeEquivalentTo(expectedAIFiles);
        }
    }
}
