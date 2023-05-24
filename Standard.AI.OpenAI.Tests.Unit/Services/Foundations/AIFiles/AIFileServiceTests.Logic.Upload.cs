// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAIFiles;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AIFiles
{
    public partial class AIFileServiceTests
    {
        [Fact]
        public async Task ShouldUploadFileAsync()
        {
            // given
            int randomCreated = GetRandomNumber();
            DateTimeOffset randomCreatedDate = CreateRandomDateTimeOffset();

            dynamic randomFileProperties = CreateRandomFileProperties(
                created: randomCreated,
                createdDate: randomCreatedDate);

            var randomAIFileRequest = new AIFileRequest
            {
                Content = randomFileProperties.Content,
                Name = randomFileProperties.Name,
                Purpose = randomFileProperties.Purpose
            };

            var randomAIFileResponse = new AIFileResponse
            {
                Id = randomFileProperties.Id,
                Type = randomFileProperties.Type,
                Size = randomFileProperties.Size,
                CreatedDate = randomFileProperties.CreatedDate,
                Name = randomFileProperties.Name,
                Purpose = randomFileProperties.Purpose,
                Status = randomFileProperties.Status,
                StatusDetails = randomFileProperties.StatusDetails
            };

            var randomAIFile = new AIFile
            {
                Request = randomAIFileRequest,
                Response = randomAIFileResponse
            };

            AIFile inputAIFile = randomAIFile;
            AIFile expectedAIFile = inputAIFile.DeepClone();

            var randomExternalAIFileRequest = new ExternalAIFileRequest
            {
                File = randomFileProperties.ExternalFile,
                Purpose = randomFileProperties.Purpose,
                FileName = randomFileProperties.FileName
            };

            var randomExternalAIFileResponse = new ExternalAIFileResponse
            {
                Id = randomFileProperties.Id,
                Object = randomFileProperties.Object,
                Bytes = randomFileProperties.Bytes,
                CreatedDate = randomFileProperties.Created,
                FileName = randomFileProperties.FileName,
                Purpose = randomFileProperties.Purpose,
                Status = randomFileProperties.ExternalStatus,
                StatusDetails = randomFileProperties.StatusDetails
            };

            this.dateTimeBrokerMock.Setup(broker =>
                broker.ConvertToDateTimeOffSet(randomCreated))
                    .Returns(randomCreatedDate);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostFileFormAsync(It.Is(
                    SameExternalAIFileRequestAs(randomExternalAIFileRequest))))
                        .ReturnsAsync(randomExternalAIFileResponse);

            // when
            AIFile actualAIFile =
                await this.aiFileService.UploadFileAsync(inputAIFile);

            // then
            actualAIFile.Should().BeEquivalentTo(expectedAIFile);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.ConvertToDateTimeOffSet(randomCreated),
                    Times.Once);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostFileFormAsync(It.Is(
                    SameExternalAIFileRequestAs(randomExternalAIFileRequest))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.openAIBrokerMock.VerifyNoOtherCalls();
        }
    }
}
