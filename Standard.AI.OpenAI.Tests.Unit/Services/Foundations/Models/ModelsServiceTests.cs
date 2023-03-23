// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Moq;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalModels;
using Standard.AI.OpenAI.Services.Foundations.Models;
using Tynamix.ObjectFiller;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.Models
{
    public partial class ModelsServiceTests
    {
        private readonly Mock<IOpenAIBroker> openAiBrokerMock;
        private readonly IModelService modelService;

        public ModelsServiceTests()
        {
            this.openAiBrokerMock = new Mock<IOpenAIBroker>();
            this.modelService = new ModelService(openAiBrokerMock.Object);
        }

        private static ExternalModelsResult CreateRandomExternalModelsResult() =>
            CreateExternalModelResultFiller().Create();

        private static Filler<ExternalModelsResult> CreateExternalModelResultFiller()
        {
            var filler = new Filler<ExternalModelsResult>();

            filler.Setup()
                .OnType<object>().IgnoreIt();

            return filler;
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();
    }
}