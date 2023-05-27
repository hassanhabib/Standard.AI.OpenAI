// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using Standard.AI.OpenAI.Clients.OpenAIs;
using Standard.AI.OpenAI.Models.Configurations;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalFineTunes;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes;
using Tynamix.ObjectFiller;
using WireMock.Server;

namespace Standard.AI.OpenAI.Tests.Acceptance.Clients.FineTunes
{
    public partial class FineTuneClientTests : IDisposable
    {
        private readonly IOpenAIClient openAIClient;
        private readonly WireMockServer wireMockServer;
        private readonly string apiKey;
        private readonly string organizationId;

        public FineTuneClientTests()
        {
            this.wireMockServer = WireMockServer.Start();
            this.apiKey = GetRandomString();
            this.organizationId = GetRandomString();

            var openAIConfiguration = new OpenAIConfigurations
            {
                ApiUrl = this.wireMockServer.Url,
                ApiKey = this.apiKey,
                OrganizationId = this.organizationId
            };

            this.openAIClient = new OpenAIClient(openAIConfiguration);
        }

        private static FineTune ConvertToFineTune(
            FineTune fineTune,
            ExternalFineTuneResponse externalFineTuneResponse)
        {
            fineTune.Response = new FineTuneResponse
            {
                CreatedDate = DateTimeOffset.FromUnixTimeSeconds(externalFineTuneResponse.CreatedDate),
                UpdatedDate = DateTimeOffset.FromUnixTimeSeconds(externalFineTuneResponse.UpdatedDate),
                FineTunedModel = externalFineTuneResponse.FineTunedModel,
                Id = externalFineTuneResponse.Id,
                Model = externalFineTuneResponse.Model,
                OrganizationId = externalFineTuneResponse.OrganizationId,
                Status = externalFineTuneResponse.Status,
                Type = externalFineTuneResponse.Object,
                ResultFiles = externalFineTuneResponse.ResultFiles,
                ValidationFiles = externalFineTuneResponse.ValidationFiles,

                HyperParameters = new HyperParameter
                {
                    BatchSize = externalFineTuneResponse.HyperParameters.BatchSize,
                    EpochsCount = externalFineTuneResponse.HyperParameters.EpochsCount,
                    LearningRateMultiplier = externalFineTuneResponse.HyperParameters.LearningRateMultiplier,
                    PromptLossWeight = externalFineTuneResponse.HyperParameters.PromptLossWeight
                },

                TrainingFiles = externalFineTuneResponse.TrainingFiles.Select(externalTrainingFile => new TrainingFile
                {
                    Id = externalTrainingFile.Id,
                    Type = externalTrainingFile.Object,
                    Purpose = externalTrainingFile.Purpose,
                    Filename = externalTrainingFile.Filename,
                    Bytes = externalTrainingFile.Bytes,
                    CreatedDate = DateTimeOffset.FromUnixTimeSeconds(externalTrainingFile.CreatedDate),
                    Status = externalTrainingFile.Status,
                    StatusDetails = externalTrainingFile.StatusDetails
                }).ToArray(),

                Events = externalFineTuneResponse.Events.Select(externalEvents => new Event
                {
                    CreatedDate = DateTimeOffset.FromUnixTimeSeconds(externalEvents.CreatedDate),
                    Level = externalEvents.Level,
                    Message = externalEvents.Message,
                    Type = externalEvents.Object
                }).ToArray()
            };

            return fineTune;
        }

        private static ExternalFineTuneRequest ConvertToFineTuneRequest(FineTune fineTune)
        {
            return new ExternalFineTuneRequest
            {
                FileId = fineTune.Request.FileId,
                BatchSize = fineTune.Request.BatchSize,
                ClassificationBetas = fineTune.Request.ClassificationBetas,
                ClassificationPositiveClass = fineTune.Request.ClassificationPositiveClass,
                ComputeClassificationMetrics = fineTune.Request.ComputeClassificationMetrics,
                Model = fineTune.Request.Model,
                NumberOfClasses = fineTune.Request.NumberOfClasses,
                Suffix = fineTune.Request.Suffix,
                LearningRateMultiplier = fineTune.Request.LearningRateMultiplier,
                NumberOfDatasetCycles = fineTune.Request.NumberOfDatasetCycles,
                PromptLossWeight = fineTune.Request.PromptLossWeight,
                ValidationFile = fineTune.Request.ValidationFile
            };
        }

        private static ExternalFineTuneResponse CreateRandomExternalFineTuneResponse() =>
            CreateExternalFineTuneResponseFiller().Create();

        private static FineTune CreateRandomFineTune() =>
            CreateRandomFineTuneFiller().Create();

        private static object[] CreateRandomObjectArray()
        {
            return Enumerable.Range(0, GetRandomNumber())
                .Select(i => GetRandomObject())
                    .ToArray();
        }

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static object GetRandomObject() =>
            GetRandomString();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static Filler<FineTune> CreateRandomFineTuneFiller()
        {
            var filler = new Filler<FineTune>();

            filler.Setup()
                .OnProperty(fineTune => fineTune.Response).IgnoreIt()
                .OnType<DateTimeOffset>().Use(GetRandomDateTime)
                .OnType<object>().Use(GetRandomObject)
                .OnProperty(fineTune => fineTune.Request.ClassificationBetas)
                    .Use(CreateRandomObjectArray);

            return filler;
        }

        private static Filler<ExternalFineTuneResponse> CreateExternalFineTuneResponseFiller()
        {
            var filler = new Filler<ExternalFineTuneResponse>();

            filler.Setup()
                .OnType<object>().Use(GetRandomObject);

            return filler;
        }

        public void Dispose() => this.wireMockServer.Stop();
    }
}
