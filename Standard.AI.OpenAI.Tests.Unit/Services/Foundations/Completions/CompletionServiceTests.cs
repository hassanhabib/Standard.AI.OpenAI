// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using RESTFulSense.Exceptions;
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
        private readonly Mock<IOpenAIBroker> openAiBrokerMock;
        private readonly ICompareLogic compareLogic;
        private readonly ICompletionService completionService;

        public CompletionServiceTests()
        {
            this.openAiBrokerMock = new Mock<IOpenAIBroker>();
            this.compareLogic = new CompareLogic();

            this.completionService = new CompletionService(
                openAiBroker: this.openAiBrokerMock.Object);
        }

        public static TheoryData UnAuthorizationExceptions()
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

        private static dynamic CreateRandomCompletionProperties()
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
                Created = GetRandomNumber(),
                ResponseModel = GetRandomString(),
                Choices = CreateRandomChoicesList(),
                Usage = CreateRandomUsage()
            };
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static string[] CreateRandomStringArray() =>
            new Filler<string[]>().Create();

        private static bool GetRandomBoolean() =>
            new SequenceGeneratorBoolean().GetValue();

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
                .OnType<object>().IgnoreIt();

            return filler;
        }
    }
}
