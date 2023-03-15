// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

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

        private static Completion CreateRandomCompletion() =>
            CreateCompletionFiller().Create();

        private static Filler<Completion> CreateCompletionFiller() =>
            new Filler<Completion>();
    }
}
