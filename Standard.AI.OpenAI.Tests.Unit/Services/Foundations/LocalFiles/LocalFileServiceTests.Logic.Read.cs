// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.IO;
using FluentAssertions;
using Moq;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.LocalFiles
{
    public partial class LocalFileServiceTests
    {
        [Fact]
        public void ShouldReadFile()
        {
            // given
            string randomFilePath = CreateRandomFilePath();
            string inputFilePath = randomFilePath;
            Stream randomStream = CreateRandomStream();
            Stream readStream = randomStream;
            Stream expectedStream = readStream;

            this.fileBrokerMock.Setup(broker =>
                broker.ReadFile(inputFilePath))
                    .Returns(readStream);

            // when
            Stream actualStream =
                this.localFileService.ReadFile(
                    inputFilePath);

            // then
            actualStream.Should().BeSameAs(expectedStream);

            this.fileBrokerMock.Verify(broker =>
                broker.ReadFile(inputFilePath),
                    Times.Once);

            this.fileBrokerMock.VerifyNoOtherCalls();
        }
    }
}
