// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Moq;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Brokers.DateTimes;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Services.Foundations.AIModels;
using Tynamix.ObjectFiller;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AIModels
{
    public partial class AIModelServiceTests
    {
        private readonly Mock<IOpenAIBroker> openAIBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly IAIModelService aiModelService;

        public AIModelServiceTests()
        {
            this.openAIBrokerMock = new Mock<IOpenAIBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();

            this.aiModelService = new AIModelService(
                openAIBroker: this.openAIBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object);
        }

        public static TheoryData UnauthorizedExceptions()
        {
            return new TheoryData<HttpResponseException>
            {
                new HttpResponseUnauthorizedException(),
                new HttpResponseForbiddenException()
            };
        }

        private static List<dynamic> CreateRandomAIModelsPropertiesList()
        {
            return Enumerable.Range(start: 0, count: GetRandomNumber())
                .Select(item =>
                {
                    return CreateRandomAIModelProperties();
                }).ToList<dynamic>();
        }

        private static dynamic CreateRandomAIModelProperties()
        {
            string idName = CreateRandomString();
            string objectType = CreateRandomString();
            string rootOriginModel = CreateRandomString();

            return new
            {
                Id = idName,
                Name = idName,
                Object = objectType,
                Type = objectType,
                Created = GetRandomDateNumber(),
                CreatedDate = GetRandomDate(),
                OwnedBy = CreateRandomString(),
                Root = rootOriginModel,
                OriginModel = rootOriginModel,
                Parent = CreateRandomString(),
                Permissions = CreateRandomPermissionPropertiesList()
            };
        }

        private static dynamic[] CreateRandomPermissionPropertiesList()
        {
            return Enumerable.Range(start: 0, count: GetRandomNumber())
                .Select(item => CreateRandomPermissionProperties())
                    .ToArray();
        }

        private static dynamic CreateRandomPermissionProperties()
        {
            string objectType = CreateRandomString();
            bool logProbabilities = GetRandomBoolean();

            return new
            {
                Id = CreateRandomString(),
                Object = objectType,
                Type = objectType,
                Created = GetRandomDateNumber(),
                CreatedDate = GetRandomDate(),
                AllowCreateEngine = GetRandomBoolean(),
                AllowSampling = GetRandomBoolean(),
                AllowLogprobs = logProbabilities,
                AllowLogProbabilities = logProbabilities,
                AllowSearchIndices = GetRandomBoolean(),
                AllowView = GetRandomBoolean(),
                AllowFineTuning = GetRandomBoolean(),
                Organization = CreateRandomString(),
                IsBlocking = GetRandomBoolean()
            };
        }

        private static DateTimeOffset GetRandomDate() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static string CreateRandomString() =>
            new MnemonicString().GetValue();

        private static int GetRandomDateNumber() =>
            new Random((int)Stopwatch.GetTimestamp()).Next(int.MinValue, int.MaxValue);

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static bool GetRandomBoolean() =>
            Randomizer<bool>.Create();
    }
}