// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using Moq;
using OpenAI.NET.Brokers.OpenAIs;
using OpenAI.NET.Models.Completions;
using OpenAI.NET.Services.Foundations.Completions;
using Tynamix.ObjectFiller;

namespace OpenAI.NET.Tests.Unit.Foundations.Completions
{
    public partial class CompletionServiceTests
    {
        private readonly Mock<IOpenAiBroker> openAiBrokerMock;
        private readonly ICompletionService completionService;

        public CompletionServiceTests()
        {
            this.openAiBrokerMock = new Mock<IOpenAiBroker>();

            this.completionService = new CompletionService(
                openAiBroker: this.openAiBrokerMock.Object);
        }

        private static dynamic CreateRandomCompletionProperties()
        {
            return new
            {
                Model = CreateRandomString(),
                Prompt = CreateRandomStringArray(),
                Suffix = CreateRandomString(),
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
                User = CreateRandomString()
            };
        }

        private static string CreateRandomString() =>
            new MnemonicString().GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 0, max: 10).GetValue();

        private static string[] CreateRandomStringArray() =>
            new Filler<string[]>().Create();

        private static bool GetRandomBoolean() =>
            new SequenceGeneratorBoolean().GetValue();

        private static Dictionary<string, int> CreateRandomDictionary() =>
            new Filler<Dictionary<string, int>>().Create();

        private static Completion CreateRandomCompletion() =>
            CreateCompletionFiller().Create();

        private static Filler<Completion> CreateCompletionFiller() =>
            new Filler<Completion>();
    }
}
