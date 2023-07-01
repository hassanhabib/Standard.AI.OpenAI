// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
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
        public async Task ShouldRemoveByIdAsync()
        {
            // given
            ExternalAIFileResponse randomDeleteAIFileResponse = 
                CreateRandomDeletedAIFileResponse();

            ExternalAIFileResponse removedAIFileResponse = 
                randomDeleteAIFileResponse;
            
            string removedFileId = removedAIFileResponse.Id;

            var expectedAIFile = 
                ConvertToAIFileOnDelete(removedAIFileResponse);

            this.wireMockServer.Given(
                Request.Create()
                .UsingDelete()
                    .WithPath($"/v1/files/{removedFileId}")
                    .WithHeader("Authorization", $"Bearer {this.apiKey}")
                    .WithHeader("OpenAI-Organization", this.organizationId))
                .RespondWith(
                    Response.Create()
                    .WithBodyAsJson(removedAIFileResponse));

            // when 
            AIFile actualAIFile =
                await this.openAIClient.AIFiles.RemoveFileByIdAsync(removedFileId);

            // then
            actualAIFile.Should().BeEquivalentTo(expectedAIFile);
        }
    }
}