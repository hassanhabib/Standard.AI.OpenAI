// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalImageGenerations;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.ImageGenerations
{
    public partial class ImageGenerationServiceTests
    {
        [Fact]
        private async Task ShouldThrowDependencyExceptionOnGenerateIfUrlNotFoundAsync()
        {
            // given
            ImageGeneration someImageGeneration = CreateRandomImageGeneration();

            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidConfigurationImageGenerationException =
                new InvalidConfigurationImageGenerationException(
                    message: "Invalid image generation configuration error occurred, contact support.",
                        innerException: httpResponseUrlNotFoundException);

            var expectedImageGenerationDependencyException =
                new ImageGenerationDependencyException(
                    message: "Image generation dependency error occurred, contact support.",
                        innerException: invalidConfigurationImageGenerationException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostImageGenerationRequestAsync(
                    It.IsAny<ExternalImageGenerationRequest>()))
                        .ThrowsAsync(httpResponseUrlNotFoundException);

            // when
            ValueTask<ImageGeneration> generateImageTask =
                this.imageGenerationService.GenerateImageAsync(someImageGeneration);

            ImageGenerationDependencyException actualImageGenerationDependencyException =
                await Assert.ThrowsAsync<ImageGenerationDependencyException>(
                    generateImageTask.AsTask);

            // then
            actualImageGenerationDependencyException.Should().BeEquivalentTo(
                expectedImageGenerationDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostImageGenerationRequestAsync(
                    It.IsAny<ExternalImageGenerationRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(UnauthorizedExceptions))]
        private async Task ShouldThrowDependencyExceptionOnGenerateIfUnauthorizedAsync(
            HttpResponseException unauthorizedException)
        {
            // given
            ImageGeneration someImageGeneration = CreateRandomImageGeneration();

            var unauthorizedImageGenerationException =
                new UnauthorizedImageGenerationException(
                    message: "Unauthorized image generation request, fix errors and try again.",
                        innerException: unauthorizedException);

            var expectedImageGenerationDependencyException =
                new ImageGenerationDependencyException(
                    message: "Image generation dependency error occurred, contact support.",
                        innerException: unauthorizedImageGenerationException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostImageGenerationRequestAsync(
                    It.IsAny<ExternalImageGenerationRequest>()))
                        .ThrowsAsync(unauthorizedException);

            // when
            ValueTask<ImageGeneration> generateImageTask =
                this.imageGenerationService.GenerateImageAsync(someImageGeneration);

            ImageGenerationDependencyException actualImageGenerationDependencyException =
                await Assert.ThrowsAsync<ImageGenerationDependencyException>(
                    generateImageTask.AsTask);

            // then
            actualImageGenerationDependencyException.Should().BeEquivalentTo(
                expectedImageGenerationDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostImageGenerationRequestAsync(
                    It.IsAny<ExternalImageGenerationRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyValidationExceptionOnGenerateIfImageGenerationNotFoundOccurredAsync()
        {
            // given
            ImageGeneration someImageGeneration = CreateRandomImageGeneration();

            var httpResponseNotFoundException =
                new HttpResponseNotFoundException();

            var notFoundImageGenerationException =
                new NotFoundImageGenerationException(
                    message: "Not found image generation error occurred, fix errors and try again.",
                        innerException: httpResponseNotFoundException);

            var expectedImageGenerationDependencyValidationException =
                new ImageGenerationDependencyValidationException(
                    message: "Image generation dependency validation error occurred, fix errors and try again.",
                        innerException: notFoundImageGenerationException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostImageGenerationRequestAsync(
                    It.IsAny<ExternalImageGenerationRequest>()))
                        .ThrowsAsync(httpResponseNotFoundException);

            // when
            ValueTask<ImageGeneration> generateImageTask =
                this.imageGenerationService.GenerateImageAsync(someImageGeneration);

            ImageGenerationDependencyValidationException actualImageGenerationDependencyValidationException =
                await Assert.ThrowsAsync<ImageGenerationDependencyValidationException>(
                    generateImageTask.AsTask);

            // then
            actualImageGenerationDependencyValidationException.Should().BeEquivalentTo(
                expectedImageGenerationDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostImageGenerationRequestAsync(
                    It.IsAny<ExternalImageGenerationRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyValidationExceptionOnGenerateIfBadRequestOccurredAsync()
        {
            // given
            ImageGeneration someImageGeneration = CreateRandomImageGeneration();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException();

            var invalidImageGenerationException =
                new InvalidImageGenerationException(
                    message: "Invalid image generation error occurred, fix errors and try again.",
                        innerException: httpResponseBadRequestException);

            var expectedImageGenerationDependencyValidationException =
                new ImageGenerationDependencyValidationException(
                    message: "Image generation dependency validation error occurred, fix errors and try again.",
                        innerException: invalidImageGenerationException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostImageGenerationRequestAsync(
                    It.IsAny<ExternalImageGenerationRequest>()))
                        .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<ImageGeneration> generateImageTask =
                this.imageGenerationService.GenerateImageAsync(someImageGeneration);

            ImageGenerationDependencyValidationException actualImageGenerationDependencyValidationException =
                await Assert.ThrowsAsync<ImageGenerationDependencyValidationException>(
                    generateImageTask.AsTask);

            // then
            actualImageGenerationDependencyValidationException.Should().BeEquivalentTo(
                expectedImageGenerationDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostImageGenerationRequestAsync(
                    It.IsAny<ExternalImageGenerationRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyValidationExceptionOnGenerateIfTooManyRequestsOccurredAsync()
        {
            // given
            ImageGeneration someImageGeneration = CreateRandomImageGeneration();

            var httpResponseTooManyRequestsException =
                new HttpResponseTooManyRequestsException();

            var excessiveCallImageGenerationException =
                new ExcessiveCallImageGenerationException(
                    message: "Excessive call error occurred, limit your calls.",
                        innerException: httpResponseTooManyRequestsException);

            var expectedImageGenerationDependencyValidationException =
                new ImageGenerationDependencyValidationException(
                    message: "Image generation dependency validation error occurred, fix errors and try again.",
                        innerException: excessiveCallImageGenerationException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostImageGenerationRequestAsync(
                    It.IsAny<ExternalImageGenerationRequest>()))
                        .ThrowsAsync(httpResponseTooManyRequestsException);

            // when
            ValueTask<ImageGeneration> generateImageTask =
                this.imageGenerationService.GenerateImageAsync(someImageGeneration);

            ImageGenerationDependencyValidationException actualImageGenerationDependencyValidationException =
                await Assert.ThrowsAsync<ImageGenerationDependencyValidationException>(
                    generateImageTask.AsTask);

            // then
            actualImageGenerationDependencyValidationException.Should().BeEquivalentTo(
                expectedImageGenerationDependencyValidationException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostImageGenerationRequestAsync(
                    It.IsAny<ExternalImageGenerationRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyExceptionOnGenerateIfHttpResponseErrorOccurredAsync()
        {
            // given
            ImageGeneration someImageGeneration = CreateRandomImageGeneration();
            var httpResponseException = new HttpResponseException();

            var failedServerImageGenerationException =
                new FailedServerImageGenerationException(
                    message: "Failed image generation server error occurred, contact support.",
                        innerException: httpResponseException);

            var expectedImageGenerationDependencyException =
                new ImageGenerationDependencyException(
                    message: "Image generation dependency error occurred, contact support.",
                        innerException: failedServerImageGenerationException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostImageGenerationRequestAsync(
                    It.IsAny<ExternalImageGenerationRequest>()))
                        .ThrowsAsync(httpResponseException);

            // when
            ValueTask<ImageGeneration> generateImageTask =
                this.imageGenerationService.GenerateImageAsync(someImageGeneration);

            ImageGenerationDependencyException actualImageGenerationDependencyException =
                await Assert.ThrowsAsync<ImageGenerationDependencyException>(
                    generateImageTask.AsTask);

            // then
            actualImageGenerationDependencyException.Should().BeEquivalentTo(
                expectedImageGenerationDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostImageGenerationRequestAsync(
                    It.IsAny<ExternalImageGenerationRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowServiceExceptionOnGenerateIfServiceErrorOccurredAsync()
        {
            // given
            ImageGeneration someImageGeneration = CreateRandomImageGeneration();
            var serviceException = new Exception();

            var failedImageGenerationServiceException =
                new FailedImageGenerationServiceException(
                    message: "Failed image generation service error occurred, contact support.",
                        innerException: serviceException);

            var expectedImageGenerationServiceException =
                new ImageGenerationServiceException(
                    message: "Image generation service error occurred, contact support.",
                        innerException: failedImageGenerationServiceException);

            this.openAIBrokerMock.Setup(broker =>
                broker.PostImageGenerationRequestAsync(
                    It.IsAny<ExternalImageGenerationRequest>()))
                        .ThrowsAsync(serviceException);
            // when
            ValueTask<ImageGeneration> generateImageTask =
                this.imageGenerationService.GenerateImageAsync(someImageGeneration);

            ImageGenerationServiceException actualImageGenerationServiceException =
                await Assert.ThrowsAsync<ImageGenerationServiceException>(
                    generateImageTask.AsTask);

            // then
            actualImageGenerationServiceException.Should().BeEquivalentTo(
                expectedImageGenerationServiceException);

            this.openAIBrokerMock.Verify(broker =>
                broker.PostImageGenerationRequestAsync(
                    It.IsAny<ExternalImageGenerationRequest>()),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}