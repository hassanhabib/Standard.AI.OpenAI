// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
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
        public async Task ShouldUploadFileAsync()
        {
            AIFileRequest randomAIFileRequest =
                CreateRandomAIFileRequest();

            var uploadAIFileRequest = new AIFile
            {
                Request = randomAIFileRequest
            };
            
            ExternalAIFileRequest externalAIFileRequest =
                ConvertToExternalAIFileRequest(uploadAIFileRequest.Request);
            
            ExternalAIFileResponse externalAIFileResponse =
                CreateExternalAiFileResponseOnUpload(externalAIFileRequest);

            AIFileResponse aiFileResponse = 
                ConvertToAIFileResponse(externalAIFileResponse);

            AIFile expectedAIFile = uploadAIFileRequest.DeepClone();
            expectedAIFile.Response = aiFileResponse;
            
            this.wireMockServer.Given(
                Request.Create()
                .UsingPost()
                    .WithPath("/v1/files")
                    .WithHeader("Authorization", $"Bearer {this.apiKey}")
                    .WithHeader("OpenAI-Organization", this.organizationId))
                .RespondWith(
                    Response.Create()
                        .WithBodyAsJson(externalAIFileResponse));
            
            // when
            AIFile actualAIFile =
                await this.openAIClient.AIFiles.UploadFileAsync(uploadAIFileRequest);

            // then
            actualAIFile.Should().BeEquivalentTo(expectedAIFile);
        }
    }
}