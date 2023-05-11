// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes;
using Standard.AI.OpenAI.Services.Foundations.FineTunes;
using Tynamix.ObjectFiller;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.FineTunes
{
    public partial class FineTuneServiceTests
    {
        private readonly Mock<IOpenAIBroker> openAIBrokerMock;
        private readonly IFineTuneService fineTuneService;

        public FineTuneServiceTests()
        {
            this.openAIBrokerMock = new Mock<IOpenAIBroker>();

            this.fineTuneService = new FineTuneService(
                openAIBroker: this.openAIBrokerMock.Object);
        }

        private static dynamic CreateRandomFineTuneProperties(
            DateTimeOffset createdDate,
            int createdDateNumber)
        {
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
                Hyperparams = CreateRandomHyperParameterProperties(),
                OrganizationId = GetRandomString(),
                TrainingFile = CreateRandomTrainingFileProperties(),
                ValidationFiles = CreateRandomObjectArray(),
                ResultFiles = CreateRandomObjectArray(),
                Created = createdDateNumber,
                CreatedDate = createdDate,
                Updated = createdDateNumber,
                UpdatedDate = createdDate,
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
            return Enumerable.Range(0, GetRandomNumber()).Select(item =>
                new
                {
                    Id = GetRandomString(),
                    Type = GetRandomString(),
                    Purpose = GetRandomString(),
                    Filename = GetRandomString(),
                    Bytes = GetRandomNumber(),
                    CreatedDate = GetRandomNumber(),
                    Status = GetRandomString(),
                    StatusDetails = GetRandomObject()
                }).ToArray();
        }

        private static dynamic[] CreateRandomEventProperties()
        {
            return Enumerable.Range(0, GetRandomNumber()).Select(item =>
                new
                {
                    Type = GetRandomString(),
                    Level = GetRandomString(),
                    Message = GetRandomString(),
                    CreatedDate = GetRandomNumber()
                }).ToArray();
        }

        private static DateTimeOffset GetRandomDate() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static object[] CreateRandomObjectArray() =>
            new Filler<object[]>().Create();

        private static bool GetRandomBoolean() =>
            Randomizer<bool>.Create();

        private static object GetRandomObject() =>
            Randomizer<object>.Create();
    }
}
