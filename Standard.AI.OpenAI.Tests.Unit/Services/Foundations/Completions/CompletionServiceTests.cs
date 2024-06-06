// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Brokers.DateTimes;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalCompletions;
using Standard.AI.OpenAI.Services.Foundations.Completions;
using Tynamix.ObjectFiller;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.Completions
{
    public partial class CompletionServiceTests
    {
        private readonly Mock<IOpenAIBroker> openAIBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly ICompareLogic compareLogic;
        private readonly ICompletionService completionService;

        public CompletionServiceTests()
        {
            this.openAIBrokerMock = new Mock<IOpenAIBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.compareLogic = new CompareLogic();

            this.completionService = new CompletionService(
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

        private Expression<Func<ExternalCompletionRequest, bool>> SameExternalCompletionRequestAs(
            ExternalCompletionRequest expectedExternalCompletionRequest)
        {
            return actualExternalCompletionRequest =>
                this.compareLogic.Compare(
                    expectedExternalCompletionRequest,
                    actualExternalCompletionRequest)
                        .AreEqual;
        }

        private static dynamic CreateRandomCompletionProperties(
            DateTimeOffset createdDate,
            int createdDateNumber)
        {
            return new
            {
                RequestModel = GetRandomString(),
                Prompts = CreateRandomStringArray(),
                Suffix = GetRandomString(),
                MaxTokens = GetRandomNumber(),
                Temperature = GetRandomNumber(),
                ProbabilityMass = GetRandomNumber(),
                CompletionsPerPrompt = GetRandomNumber(),
                Stream = GetRandomBoolean(),
                LogProbabilities = GetRandomNumber(),
                Echo = GetRandomBoolean(),
                Stop = CreateRandomStringArray(),
                PresencePenalty = GetRandomNumber(),
                FrequencyPenalty = GetRandomNumber(),
                BestOf = GetRandomNumber(),
                LogitBias = CreateRandomDictionary(),
                User = GetRandomString(),
                Id = GetRandomString(),
                Object = GetRandomString(),
                CreatedDate = createdDate,
                Created = createdDateNumber,
                ResponseModel = GetRandomString(),
                Choices = CreateRandomChoicesList(),
                Usage = CreateRandomUsage()
            };
        }

        private static DateTimeOffset GetRandomDate() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static string[] CreateRandomStringArray() =>
            new Filler<string[]>().Create();

        private static bool GetRandomBoolean() =>
            Randomizer<bool>.Create();

        private static Dictionary<string, int> CreateRandomDictionary() =>
            new Filler<Dictionary<string, int>>().Create();

        private static Completion CreateRandomCompletion() =>
            CreateCompletionFiller().Create();

        private static dynamic[] CreateRandomChoicesList()
        {
            return Enumerable.Range(0, GetRandomNumber()).Select(
                item => new
                {
                    Text = GetRandomString(),
                    Index = GetRandomNumber(),
                    LogProbabilities = GetRandomNumber(),
                    FinishReason = GetRandomString()
                }).ToArray();
        }

        private static dynamic CreateRandomUsage()
        {
            return new
            {
                PromptTokens = GetRandomNumber(),
                CompletionTokens = GetRandomNumber(),
                TotalTokens = GetRandomNumber(),
            };
        }

        private static Filler<Completion> CreateCompletionFiller()
        {
            var filler = new Filler<Completion>();

            filler.Setup()
                .OnType<object>().IgnoreIt()
                .OnType<DateTimeOffset>().Use(GetRandomDate());

            return filler;
        }
    }
}
