// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using Moq;
using Standard.AI.OpenAI.Brokers.DateTimes;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Services.Foundations.AIModels;
using Tynamix.ObjectFiller;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AIModels
{
    public partial class AIModelServiceTests
    {
        private readonly Mock<IOpenAIBroker> openAIBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly IAIModelService aIModelService;

        public AIModelServiceTests()
        {
            this.openAIBrokerMock = new Mock<IOpenAIBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();

            this.aIModelService = new AIModelService(
                openAIBroker: this.openAIBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object);
        }

        private static List<dynamic> CreateRandomAIModelsProperties()
        {
            return new List<dynamic>
            {

            };
        }

        private static dynamic CreateRandomPermissionProperties()
        {
            bool randomAllowLogProbabilities = GetRandomBoolean();

            return new
            {
                Id = GetRandomString(),
                Created = GetRandomNumber(),
                CreatedDate = 
                AllowCreateEngine = GetRandomBoolean(),
                AllowSampling = GetRandomBoolean(),
                AllowLogprobs = randomAllowLogProbabilities,
                AllowLogProbabilities = randomAllowLogProbabilities,
                AllowSearchIndices = GetRandomBoolean(),
                AllowView = GetRandomBoolean(),
                AllowFineTuning = GetRandomBoolean(),
                Organization = GetRandomString(),
                IsBlocking = GetRandomBoolean()
            };
        }

        private static string GetRandomString() =>
           new MnemonicString().GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static bool GetRandomBoolean() =>
            new SequenceGeneratorBoolean().GetValue();
    }
}
