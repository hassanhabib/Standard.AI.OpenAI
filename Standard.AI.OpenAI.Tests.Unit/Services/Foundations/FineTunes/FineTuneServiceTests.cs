// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Standard.AI.OpenAI.Brokers.DateTimes;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalFineTunes;
using Standard.AI.OpenAI.Services.Foundations.FineTunes;
using Tynamix.ObjectFiller;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.FineTunes
{
    public partial class FineTuneServiceTests
    {
        private readonly Mock<IOpenAIBroker> openAIBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly ICompareLogic compareLogic;
        private readonly IFineTuneService fineTuneService;

        public FineTuneServiceTests()
        {
            this.openAIBrokerMock = new Mock<IOpenAIBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.compareLogic = new CompareLogic();

            this.fineTuneService = new FineTuneService(
                openAIBroker: this.openAIBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object);

        }

        private static dynamic CreateRandomFineTuneProperties()
        {
            DateTimeOffset randomCreatedDateTime = GetRandomDate();
            DateTimeOffset randomUpdatedDateTime = GetRandomDate();
            int randomCreatedUnixEpoch = GetRandomNumber();
            int randomUpdatedUnixEpoch = GetRandomNumber();

            return new
            {
                FileId = GetRandomString(),
                ValidationFile = GetRandomString(),
                Model = GetRandomString(),
                NumberOfDatasetCycles = GetRandomString(),
                BatchSize = GetRandomNumber(),
                LearningRateMultiplier = GetRandomNumber(),
                PromptLossWeight = GetRandomNumber(),
                ComputeClassificationMetrics = GetRandomBoolean(),
                NumberOfClasses = GetRandomNumber(),
                ClassificationPositiveClass = GetRandomString(),
                ClassificationBetas = CreateRandomObjectArray(),
                Suffix = GetRandomString(),
                Id = GetRandomString(),
                Type = GetRandomString(),
                HyperParameters = CreateRandomHyperParameterProperties(),
                OrganizationId = GetRandomString(),
                TrainingFile = CreateRandomTrainingFileProperties(),
                ValidationFiles = CreateRandomObjectArray(),
                ResultFiles = CreateRandomObjectArray(),
                Created = randomCreatedUnixEpoch,
                CreatedDate = randomCreatedDateTime,
                Updated = randomUpdatedUnixEpoch,
                UpdatedDate = randomUpdatedDateTime,
                Status = GetRandomString(),
                FineTunedModel = GetRandomObject(),
                Events = CreateRandomEventProperties()
            };
        }

        private static dynamic CreateRandomHyperParameterProperties()
        {
            return new
            {
                EpochsCount = GetRandomNumber(),
                BatchSize = GetRandomObject(),
                PromptLossWeight = GetRandomNumber(),
                LearningRateMultiplier = GetRandomObject()
            };
        }

        private static dynamic[] CreateRandomTrainingFileProperties()
        {
            DateTimeOffset randomCreatedDateTime = GetRandomDate();
            int randomCreatedUnixEpoch = GetRandomNumber();

            return Enumerable.Range(0, GetRandomNumber()).Select(item =>
                new
                {
                    Id = GetRandomString(),
                    Type = GetRandomString(),
                    Purpose = GetRandomString(),
                    Filename = GetRandomString(),
                    Bytes = GetRandomNumber(),
                    CreatedDate = randomCreatedDateTime,
                    Created = randomCreatedUnixEpoch,
                    Status = GetRandomString(),
                    StatusDetails = GetRandomObject()
                }).ToArray();
        }

        private static dynamic[] CreateRandomEventProperties()
        {
            DateTimeOffset randomCreatedDateTime = GetRandomDate();
            int randomCreatedUnixEpoch = GetRandomNumber();

            return Enumerable.Range(0, GetRandomNumber()).Select(item =>
                new
                {
                    Type = GetRandomString(),
                    Level = GetRandomString(),
                    Message = GetRandomString(),
                    Created = randomCreatedUnixEpoch,
                    CreatedDate = randomCreatedDateTime
                }).ToArray();
        }

        private static DateTimeOffset GetRandomDate() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static object[] CreateRandomObjectArray()
        {
            return Enumerable.Range(0, GetRandomNumber())
                .Select(i => GetRandomObject())
                    .ToArray();
        }

        private static bool GetRandomBoolean() =>
            Randomizer<bool>.Create();

        private static object GetRandomObject() => 
            GetRandomString();

        private Expression<Func<ExternalFineTuneRequest, bool>> SameExternalFineTuneRequestAs(
           ExternalFineTuneRequest expectedExternalFineTuneRequest)
        {
            return actualExternalFineTuneRequest =>
                this.compareLogic.Compare(
                    expectedExternalFineTuneRequest,
                    actualExternalFineTuneRequest)
                        .AreEqual;
        }
    }
}
