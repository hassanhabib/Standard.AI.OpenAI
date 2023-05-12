// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalFineTunes;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.FineTunes
{
    public partial class FineTuneServiceTests
    {
        [Fact]
        public async Task ShouldSubmitFineTuneAsync()
        {
            // given
            dynamic randomFineTuneProperties = CreateRandomFineTuneProperties();

            var inputFineTuneRequest = new FineTuneRequest
            {
                FileId = randomFineTuneProperties.FileId,
                ValidationFile = randomFineTuneProperties.ValidationFile,
                Model = randomFineTuneProperties.Model,
                NumberOfDatasetCycles = randomFineTuneProperties.NumberOfDatasetCycles,
                BatchSize = randomFineTuneProperties.BatchSize,
                LearningRateMultiplier = randomFineTuneProperties.LearningRateMultiplier,
                PromptLossWeight = randomFineTuneProperties.PromptLossWeight,
                ComputeClassificationMetrics = randomFineTuneProperties.ComputeClassificationMetrics,
                NumberOfClasses = randomFineTuneProperties.NumberOfClasses,
                ClassificationPositiveClass = randomFineTuneProperties.ClassificationPositiveClass,
                ClassificationBetas = randomFineTuneProperties.ClassificationBetas,
                Suffix = randomFineTuneProperties.Suffix
            };

            var externalFineTuneRequest = new ExternalFineTuneRequest
            {
                FileId = randomFineTuneProperties.FileId,
                ValidationFile = randomFineTuneProperties.ValidationFile,
                Model = randomFineTuneProperties.Model,
                NumberOfDatasetCycles = randomFineTuneProperties.NumberOfDatasetCycles,
                BatchSize = randomFineTuneProperties.BatchSize,
                LearningRateMultiplier = randomFineTuneProperties.LearningRateMultiplier,
                PromptLossWeight = randomFineTuneProperties.PromptLossWeight,
                ComputeClassificationMetrics = randomFineTuneProperties.ComputeClassificationMetrics,
                NumberOfClasses = randomFineTuneProperties.NumberOfClasses,
                ClassificationPositiveClass = randomFineTuneProperties.ClassificationPositiveClass,
                ClassificationBetas = randomFineTuneProperties.ClassificationBetas,
                Suffix = randomFineTuneProperties.Suffix
            };

            var inputFineTune = new FineTune();
            inputFineTune.Request = inputFineTuneRequest;
            FineTune expectedFineTune = inputFineTune.DeepClone();
            dynamic[] trainingFileProperties = randomFineTuneProperties.TrainingFile;

            TrainingFile[] trainingFiles = trainingFileProperties.Select(trainingFileProperty =>
                new TrainingFile
                {
                    Id = trainingFileProperty.Id,
                    Type = trainingFileProperty.Type,
                    Purpose = trainingFileProperty.Purpose,
                    Filename = trainingFileProperty.Filename,
                    Bytes = trainingFileProperty.Bytes,
                    CreatedDate = (DateTimeOffset)trainingFileProperty.CreatedDate,
                    Status = trainingFileProperty.Status,
                    StatusDetails = trainingFileProperty.StatusDetails
                }).ToArray();

            dynamic[] randomEventProperties = (dynamic[])randomFineTuneProperties.Events;

            Event[] events = randomEventProperties.Select(eventProperties =>
                new Event
                {
                    CreatedDate = (DateTimeOffset)eventProperties.CreatedDate,
                    Level = eventProperties.Level,
                    Message = eventProperties.Message,
                    Type = eventProperties.Type
                }).ToArray();

            expectedFineTune.Response = new FineTuneResponse
            {
                Id = randomFineTuneProperties.Id,
                Type = randomFineTuneProperties.Type,

                HyperParameters = new HyperParameter
                {
                    EpochsCount = randomFineTuneProperties.HyperParameters.EpochsCount,
                    BatchSize = randomFineTuneProperties.HyperParameters.BatchSize,
                    PromptLossWeight = randomFineTuneProperties.HyperParameters.PromptLossWeight,
                    LearningRateMultiplier = randomFineTuneProperties.HyperParameters.LearningRateMultiplier
                },

                OrganizationId = randomFineTuneProperties.OrganizationId,
                Model = randomFineTuneProperties.Model,
                TrainingFiles = trainingFiles,
                ValidationFiles = randomFineTuneProperties.ValidationFiles,
                ResultFiles = randomFineTuneProperties.ResultFiles,
                CreatedDate = (DateTimeOffset)randomFineTuneProperties.CreatedDate,
                UpdatedDate = (DateTimeOffset)randomFineTuneProperties.UpdatedDate,
                Status = randomFineTuneProperties.Status,
                FineTunedModel = randomFineTuneProperties.FineTunedModel,
                Events = events
            };

            ExternalTrainingFile[] externalTrainingFiles = trainingFileProperties.Select(trainingFileProperty =>
                new ExternalTrainingFile
                {
                    Id = trainingFileProperty.Id,
                    Object = trainingFileProperty.Type,
                    Purpose = trainingFileProperty.Purpose,
                    Filename = trainingFileProperty.Filename,
                    Bytes = trainingFileProperty.Bytes,
                    CreatedDate = (int)trainingFileProperty.Created,
                    Status = trainingFileProperty.Status,
                    StatusDetails = trainingFileProperty.StatusDetails
                }).ToArray();

            ExternalEvent[] externalRandomEventProperties = randomEventProperties.Select(eventProperties =>
                new ExternalEvent
                {
                    CreatedDate = (int)eventProperties.Created,
                    Level = eventProperties.Level,
                    Message = eventProperties.Message,
                    Object = eventProperties.Type
                }).ToArray();

            var externalHyperParameters = new ExternalHyperParameters
            {
                EpochsCount = randomFineTuneProperties.HyperParameters.EpochsCount,
                BatchSize = randomFineTuneProperties.HyperParameters.BatchSize,
                PromptLossWeight = randomFineTuneProperties.HyperParameters.PromptLossWeight,
                LearningRateMultiplier = randomFineTuneProperties.HyperParameters.LearningRateMultiplier
            };

            var externalFineTuneResponse = new ExternalFineTuneResponse
            {
                Object = randomFineTuneProperties.Type,
                Id = randomFineTuneProperties.Id,
                HyperParameters = externalHyperParameters,
                OrganizationId = randomFineTuneProperties.OrganizationId,
                Model = randomFineTuneProperties.Model,
                TrainingFiles = externalTrainingFiles,
                ValidationFiles = randomFineTuneProperties.ValidationFiles,
                ResultFiles = randomFineTuneProperties.ResultFiles,
                CreatedDate = (int)randomFineTuneProperties.Created,
                UpdatedDate = (int)randomFineTuneProperties.Updated,
                Status = randomFineTuneProperties.Status,
                FineTunedModel = randomFineTuneProperties.FineTunedModel,
                Events = externalRandomEventProperties
            };

            this.openAIBrokerMock.Setup(broker =>
                broker.PostFineTuneAsync(It.Is(
                    SameExternalFineTuneRequestAs(externalFineTuneRequest))))
                        .ReturnsAsync(value: externalFineTuneResponse);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.ConvertToDateTimeOffSet(externalFineTuneResponse.CreatedDate))
                    .Returns(expectedFineTune.Response.CreatedDate);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.ConvertToDateTimeOffSet(externalFineTuneResponse.UpdatedDate))
                    .Returns(expectedFineTune.Response.UpdatedDate);

            foreach (var fineTuneFileProperties in trainingFileProperties)
            {
                int inputEpochCreated = (int)fineTuneFileProperties.Created;

                DateTimeOffset expectedEpochConvertedCreatedDate =
                    (DateTimeOffset)fineTuneFileProperties.CreatedDate;

                this.dateTimeBrokerMock.Setup(broker =>
                    broker.ConvertToDateTimeOffSet(inputEpochCreated))
                        .Returns(expectedEpochConvertedCreatedDate);
            }

            foreach (var fineTuneEventProperty in randomEventProperties)
            {
                int inputEpochCreated = (int)fineTuneEventProperty.Created;

                DateTimeOffset expectedEpochConvertedDate =
                    (DateTimeOffset)fineTuneEventProperty.CreatedDate;

                this.dateTimeBrokerMock.Setup(broker =>
                    broker.ConvertToDateTimeOffSet(inputEpochCreated))
                        .Returns(expectedEpochConvertedDate);
            }

            // when
            FineTune actualFineTune =
                await this.fineTuneService.SubmitFineTuneAsync(inputFineTune);

            // then
            actualFineTune.Should().BeEquivalentTo(expectedFineTune);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostFineTuneAsync(It.Is(
                    SameExternalFineTuneRequestAs(externalFineTuneRequest))),
                        Times.Once());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.ConvertToDateTimeOffSet(externalFineTuneResponse.CreatedDate),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.ConvertToDateTimeOffSet(externalFineTuneResponse.UpdatedDate),
                    Times.Once);

            foreach (var fineTuneFileProperties in trainingFileProperties)
            {
                int inputEpochCreated = (int)fineTuneFileProperties.Created;

                DateTimeOffset expectedEpochConvertedDate =
                    (DateTimeOffset)fineTuneFileProperties.CreatedDate;

                this.dateTimeBrokerMock.Verify(broker =>
                    broker.ConvertToDateTimeOffSet(inputEpochCreated),
                        Times.Once);
            }

            foreach (var fineTuneEventProperty in randomEventProperties)
            {
                int inputEpochCreated = (int)fineTuneEventProperty.Created;

                DateTimeOffset expectedEpochConvertedDate =
                    (DateTimeOffset)fineTuneEventProperty.CreatedDate;

                this.dateTimeBrokerMock.Verify(broker =>
                    broker.ConvertToDateTimeOffSet(inputEpochCreated),
                        Times.Once);
            }

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
