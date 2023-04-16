// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Standard.AI.OpenAI.Clients.OpenAIs;
using Standard.AI.OpenAI.Models.Configurations;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAIModels;
using Tynamix.ObjectFiller;
using WireMock.Server;

namespace Standard.AI.OpenAI.Tests.Acceptance.Clients.AIModels
{
    public partial class AIModelsClientTests : IDisposable
    {
        private readonly string apiKey;
        private readonly string organizationId;
        private readonly WireMockServer wireMockServer;
        private readonly IOpenAIClient openAIClient;

        public AIModelsClientTests()
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

        private static IEnumerable<AIModel> ConvertToAIModels(ExternalAIModelsResult externalAIModelsResult) =>
            externalAIModelsResult.AIModels.Select(ConvertToAIModel).ToArray();

        private static AIModel ConvertToAIModel(ExternalAIModel externalAIModel)
        {
            return new AIModel
            {
                Name = externalAIModel.Id,
                CreatedDate = DateTimeOffset.FromUnixTimeSeconds(externalAIModel.Created),
                Type = externalAIModel.Object,
                OwnedBy = externalAIModel.OwnedBy,
                Parent = externalAIModel.Parent,
                OriginModel = externalAIModel.Root,

                Permissions = externalAIModel.Permissions.Select(
                    externalPermission =>
                    {
                        return new AIModelPermission
                        {
                            Id = externalPermission.Id,
                            Type = externalPermission.Object,
                            CreatedDate = DateTimeOffset.FromUnixTimeSeconds(externalPermission.Created),
                            AllowCreateEngine = externalPermission.AllowCreateEngine,
                            AllowSampling = externalPermission.AllowSampling,
                            AllowLogProbabilities = externalPermission.AllowLogprobs,
                            AllowSearchIndices = externalPermission.AllowSearchIndices,
                            AllowView = externalPermission.AllowView,
                            AllowFineTuning = externalPermission.AllowFineTuning,
                            Organization = externalPermission.Organization,
                            IsBlocking = externalPermission.IsBlocking
                        };
                    }).ToArray()
            };
        }

        private static ExternalAIModel CreateRandomExternalAIModel() =>
            CreateExternalAIModelFiller().Create();

        private static ExternalAIModelsResult CreateRandomExternalAIModelsResult() =>
            CreateExternalAIModelResultFiller().Create();

        private static Filler<ExternalAIModel> CreateExternalAIModelFiller() =>
            new Filler<ExternalAIModel>();

        private static Filler<ExternalAIModelsResult> CreateExternalAIModelResultFiller() =>
            new Filler<ExternalAIModelsResult>();

        public void Dispose() => this.wireMockServer.Stop();
    }
}