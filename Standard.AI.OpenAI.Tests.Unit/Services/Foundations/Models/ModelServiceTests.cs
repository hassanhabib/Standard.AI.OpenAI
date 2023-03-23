// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Moq;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalModels;
using Standard.AI.OpenAI.Services.Foundations.Models;
using Tynamix.ObjectFiller;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.Models
{
    public partial class ModelServiceTests
    {
        private readonly Mock<IOpenAIBroker> openAiBrokerMock;
        private readonly IModelService modelService;

        public ModelServiceTests()
        {
            this.openAiBrokerMock = new Mock<IOpenAIBroker>();
            this.modelService = new ModelService(openAiBrokerMock.Object);
        }
        public static TheoryData UnAuthorizationExceptions()
        {
            return new TheoryData<HttpResponseException>
            {
                new HttpResponseUnauthorizedException(),
                new HttpResponseForbiddenException()
            };
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