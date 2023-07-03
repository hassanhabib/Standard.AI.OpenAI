// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Moq;
using Standard.AI.OpenAI.Clients.OpenAIs;
using Standard.AI.OpenAI.Models.Configurations;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAIFiles;
using Tynamix.ObjectFiller;
using WireMock.Server;

namespace Standard.AI.OpenAI.Tests.Acceptance.Clients.AIFiles
{
    public partial class AIFilesClientTests : IDisposable
    {
        private readonly string apiKey;
        private readonly string organizationId;
        private readonly WireMockServer wireMockServer;
        private readonly IOpenAIClient openAIClient;

        public AIFilesClientTests()
        {
            this.apiKey = CreateRandomString();
            this.organizationId = CreateRandomString();
            this.wireMockServer = WireMockServer.Start();

            var openAIConfigurations = new OpenAIConfigurations
            {
                ApiUrl = this.wireMockServer.Url,
                ApiKey = this.apiKey,
                OrganizationId = this.organizationId
            };

            this.openAIClient = new OpenAIClient(openAIConfigurations);
        }

        private static string CreateRandomString() =>
            new MnemonicString().GetValue();

        private static IEnumerable<AIFileResponse> ConvertToAIFiles(ExternalAIFilesResult externalAIFilesResult) =>
            externalAIFilesResult.Files.Select(ConvertToAIFileResponse).ToArray();

        private static AIFileResponse ConvertToAIFileResponse(ExternalAIFileResponse externalAIFileResponse)
        {
            return new AIFileResponse
            {
                Id = externalAIFileResponse.Id,
                Type = externalAIFileResponse.Object,
                Size = externalAIFileResponse.Bytes,
                CreatedDate = DateTimeOffset.FromUnixTimeSeconds(externalAIFileResponse.CreatedDate),
                Name = externalAIFileResponse.FileName,
                Purpose = externalAIFileResponse.Purpose,
                Deleted = externalAIFileResponse.Deleted,
                Status = ConvertToAIFileStatus(externalAIFileResponse.Status),
                StatusDetails = externalAIFileResponse.StatusDetails
            };
        }
        
        private static ExternalAIFileRequest ConvertToExternalAIFileRequest(AIFileRequest aiFileRequest)
        {
            var externalAIFileRequest = new ExternalAIFileRequest
            {
                Purpose = aiFileRequest.Purpose,
                FileName = aiFileRequest.Name,
                File = aiFileRequest.Content
            };

            return externalAIFileRequest;
        }
        
        private static AIFile ConvertToAIFileOnDelete(ExternalAIFileResponse externalAiFileResponse)
        {
            AIFileResponse response = ConvertToAIFileResponse(externalAiFileResponse);
            response.CreatedDate = default;

            var aiFile = new AIFile();
            aiFile.Response = response;

            return aiFile;
        }

        private static AIFileStatus ConvertToAIFileStatus(string externalStatus)
        {
            return externalStatus?.ToLowerInvariant() switch
            {
                "uploaded" => AIFileStatus.Uploaded,
                "processed" => AIFileStatus.Processed,
                "error" => AIFileStatus.Error,
                _ => AIFileStatus.Unknown
            };
        }
        
        private static ExternalAIFileResponse CreateExternalAiFileResponseOnUpload(
            ExternalAIFileRequest externalAiFileRequest
        )
        {
            Filler<ExternalAIFileResponse> externalAiFileResponseFiller = CreateExternalAIFileResponseFiller();
            externalAiFileResponseFiller.Setup()
                .OnProperty(response => response.FileName)
                .Use(externalAiFileRequest.FileName)
                .OnProperty(response => response.Purpose)
                .Use(externalAiFileRequest.Purpose);

            return externalAiFileResponseFiller.Create();
        }

        private static AIFileRequest CreateRandomAIFileRequest()
        {
            var aiFileRequestFiller = CreateRandomAIFileRequestFiller();
            aiFileRequestFiller.Setup()
                .OnProperty(request => request.Content)
                    .Use(CreateRandomStream());

            return aiFileRequestFiller.Create();
        }
        
        private static ExternalAIFileResponse CreateRandomDeletedAIFileResponse()
        {
            var filler = CreateExternalAIFileResponseFiller();
            filler.Setup()
                .OnProperty(response => response.Bytes)
                    .IgnoreIt()
                .OnProperty(response => response.CreatedDate)
                    .IgnoreIt()
                .OnProperty(response => response.FileName)
                    .IgnoreIt()
                .OnProperty(response => response.Purpose)
                    .IgnoreIt()
                .OnProperty(response => response.Status)
                    .IgnoreIt()
                .OnProperty(response => response.StatusDetails)
                    .IgnoreIt();

            filler.Setup()
                .OnProperty(response => response.Deleted)
                    .Use(true);

            return filler.Create();
        }
        
        private static Stream CreateRandomStream()
        {
            var mockStream = new Mock<MemoryStream>();

            mockStream.SetupGet(stream =>
                    stream.ReadTimeout)
                .Returns(0);

            mockStream.SetupGet(stream =>
                    stream.WriteTimeout)
                .Returns(0);

            return mockStream.Object;
        }

        private static ExternalAIFilesResult CreateRandomExternalAIFilesResult() =>
            CreateExternalAIFilesResultFiller().Create();
        
        private static Filler<AIFileRequest> CreateRandomAIFileRequestFiller() =>
            new Filler<AIFileRequest>();
        
        private static Filler<ExternalAIFilesResult> CreateExternalAIFilesResultFiller() =>
            new Filler<ExternalAIFilesResult>();

        private static Filler<ExternalAIFileResponse> CreateExternalAIFileResponseFiller() =>
            new Filler<ExternalAIFileResponse>();

        public void Dispose() => this.wireMockServer.Stop();
    }
}
