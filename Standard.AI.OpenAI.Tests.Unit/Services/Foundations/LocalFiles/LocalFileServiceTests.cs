// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.IO;
using System.Text;
using Moq;
using Standard.AI.OpenAI.Brokers.Files;
using Standard.AI.OpenAI.Services.Foundations.LocalFiles;
using Tynamix.ObjectFiller;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.LocalFiles
{
    public partial class LocalFileServiceTests
    {
        private readonly Mock<IFileBroker> fileBrokerMock;
        private readonly ILocalFileService localFileService;

        public LocalFileServiceTests()
        {
            this.fileBrokerMock = new Mock<IFileBroker>();

            this.localFileService = new LocalFileService(
                fileBroker: this.fileBrokerMock.Object);
        }

        public static TheoryData FileValidationExceptions()
        {
            return new TheoryData<Exception>()
            {
                new ArgumentException(),
                new PathTooLongException()
            };
        }

        public static TheoryData FileNotFoundExceptions()
        {
            return new TheoryData<Exception>()
            {
                new FileNotFoundException(),
                new DirectoryNotFoundException()
            };
        }

        public static TheoryData FileDependencyExceptions()
        {
            return new TheoryData<Exception>()
            {
                new IOException(),
                new NotSupportedException()
            };
        }

        private static string CreateRandomFilePath() =>
            new MnemonicString().GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private Stream CreateRandomStream()
        {
            int randomWordCount = GetRandomNumber();

            string randomContent =
                new MnemonicString(randomWordCount)
                    .GetValue();

            byte[] buffer = Encoding.UTF8.GetBytes(randomContent);
            var memoryStream = new MemoryStream(buffer);

            return memoryStream;
        }
    }
}
