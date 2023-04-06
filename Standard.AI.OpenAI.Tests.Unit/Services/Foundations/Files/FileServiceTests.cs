// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Moq;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Services.Foundations.Files;
using Tynamix.ObjectFiller;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.Files
{
    public partial class FileServiceTests
    {
        private readonly Mock<IOpenAIBroker> openAIBrokerMock;
        private readonly IFileService fileService;

        public FileServiceTests()
        {
            this.openAIBrokerMock = new Mock<IOpenAIBroker>();

            this.fileService = new FileService(
                openAIBroker: this.openAIBrokerMock.Object);
        }

        public static TheoryData UnauthorizedExceptions()
        {
            return new TheoryData<HttpResponseException>
            {
                new HttpResponseUnauthorizedException(),
                new HttpResponseForbiddenException()
            };
        }

        private static dynamic CreateRandomFileProperties()
        {
            string objectType = GetRandomString();

            return new
            {
                Id = GetRandomString(),
                Object = objectType,
                Type = objectType,
                Deleted = GetRandomBoolean()
            };
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static bool GetRandomBoolean() =>
            Randomizer<bool>.Create();
    }
}