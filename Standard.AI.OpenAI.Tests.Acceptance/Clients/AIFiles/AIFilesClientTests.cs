// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
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

        private static ExternalAIFilesResult CreateRandomExternalAIFilesResult() =>
            CreateExternalAIFilesResultFiller().Create();
        
        private static Filler<ExternalAIFilesResult> CreateExternalAIFilesResultFiller() =>
            new Filler<ExternalAIFilesResult>();

        private static Filler<ExternalAIFileResponse> CreateExternalAIFileResponseFiller() =>
            new Filler<ExternalAIFileResponse>();

        public void Dispose() => this.wireMockServer.Stop();
    }
}
