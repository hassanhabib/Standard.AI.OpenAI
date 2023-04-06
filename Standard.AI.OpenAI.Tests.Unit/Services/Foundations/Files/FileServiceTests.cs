// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Moq;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Services.Foundations.Files;
using Tynamix.ObjectFiller;

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