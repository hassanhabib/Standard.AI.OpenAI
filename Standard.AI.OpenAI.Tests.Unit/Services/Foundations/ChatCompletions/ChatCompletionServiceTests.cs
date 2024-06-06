// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Brokers.DateTimes;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalChatCompletions;
using Standard.AI.OpenAI.Services.Foundations.ChatCompletions;
using Tynamix.ObjectFiller;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.ChatCompletions
{
    public partial class ChatCompletionServiceTests
    {
        private readonly Mock<IOpenAIBroker> openAIBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly ICompareLogic compareLogic;
        private readonly IChatCompletionService chatCompletionService;

        public ChatCompletionServiceTests()
        {
            this.openAIBrokerMock = new Mock<IOpenAIBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.compareLogic = new CompareLogic();

            this.chatCompletionService = new ChatCompletionService(
                openAIBroker: this.openAIBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object);
        }

        private static dynamic CreateRandomChatCompletionProperties(
            DateTimeOffset createdDate,
            int createdDateNumber)
        {
            return new
            {
                Model = GetRandomString(),
                Messages = GetRandomChatCompletionMessages(),
                Temperature = GetRandomNumber(),
                ProbabilityMass = GetRandomNumber(),
                CompletionsPerPrompt = GetRandomNumber(),
                Stream = GetRandomBoolean(),
                Stop = CreateRandomStringArray(),
                MaxTokens = GetRandomNumber(),
                PresencePenalty = GetRandomNumber(),
                FrequencyPenalty = GetRandomNumber(),
                LogitBias = CreateRandomDictionary(),
                User = GetRandomString(),
                Id = GetRandomString(),
                Object = GetRandomString(),
                CreatedDate = createdDate,
                Created = createdDateNumber,
                Choices = GetRandomChatCompletionChoices(),
                Usage = GetRandomChatCompletionUsage()
            };
        }
        private Expression<Func<ExternalChatCompletionRequest, bool>> SameExternalChatCompletionRequestAs(
            ExternalChatCompletionRequest expectedExternalChatCompletionRequest)
        {
            return actualExternalCompletionRequest =>
                this.compareLogic.Compare(
                    expectedExternalChatCompletionRequest,
                    actualExternalCompletionRequest)
                        .AreEqual;
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

        private static dynamic GetRandomChatCompletionMessage()
        {
            return new
            {
                Role = GetRandomString(),
                Content = GetRandomString()
            };
        }

        private static dynamic GetRandomChatCompletionUsage()
        {
            return new
            {
                PromptTokens = GetRandomNumber(),
                CompletionTokens = GetRandomNumber(),
                TotalTokens = GetRandomNumber()
            };
        }

        private static dynamic[] GetRandomChatCompletionChoices()
        {
            return Enumerable.Range(0, GetRandomNumber()).Select(
                item => new
                {
                    Index = GetRandomNumber(),
                    Message = GetRandomChatCompletionMessage(),
                    FinishReason = GetRandomString()
                }).ToArray();
        }

        private static dynamic[] GetRandomChatCompletionMessages()
        {
            return Enumerable.Range(0, GetRandomNumber()).Select(
                item => new
                {
                    Role = GetRandomString(),
                    Content = GetRandomString()
                }).ToArray();
        }

        public static TheoryData UnauthorizedExceptions()
        {
            return new TheoryData<HttpResponseException>
            {
                new HttpResponseUnauthorizedException(),
                new HttpResponseForbiddenException()
            };
        }

        private static ChatCompletion CreateRandomChatCompletion() =>
            ChatCompletionFiller().Create();

        private static Filler<ChatCompletion> ChatCompletionFiller()
        {
            var filler = new Filler<ChatCompletion>();

            filler.Setup()
                .OnType<object>().IgnoreIt()
                .OnType<DateTimeOffset>().Use(GetRandomDate());

            return filler;
        }
    }
}
