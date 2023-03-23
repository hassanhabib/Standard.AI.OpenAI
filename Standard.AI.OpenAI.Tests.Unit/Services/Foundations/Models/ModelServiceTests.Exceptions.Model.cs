// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalCompletions;
using Standard.AI.OpenAI.Models.Services.Foundations.Models;
using Standard.AI.OpenAI.Models.Services.Foundations.Models.Exceptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.Models
{
    public partial class ModelServiceTests
    {
        [Theory]
        [MemberData(nameof(UnAuthorizationExceptions))]
        public async Task ShouldThrowDependencyExceptionOnGetModelsIfUnAuthorizedAsync(
            HttpResponseException unAuthorizationException)
        {

            // given
            var unauthorizedModelException =
                new UnauthorizedModelException(
                    unAuthorizationException);

            var expectedModelDependencyException =
                new ModelDependencyException(
                    unauthorizedModelException);

            this.openAiBrokerMock.Setup(broker =>
                broker.GetAllModelsAsync())
                        .ThrowsAsync(unAuthorizationException);

            // when
            ValueTask<Model[]> getModelsTask = this.modelService.GetModelsAsync();

            ModelDependencyException actualModelDependencyException =
                await Assert.ThrowsAsync<ModelDependencyException>(
                    getModelsTask.AsTask);

            // then
            actualModelDependencyException.Should().BeEquivalentTo(
                expectedModelDependencyException);

            this.openAiBrokerMock.Verify(broker =>
                broker.GetAllModelsAsync(), Times.Once);

            this.openAiBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnGetModelsIfUrlNotFoundErrorOccursAsync()
        {
            // given
            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidConfigurationModelException =
                new InvalidConfigurationModelException(
                    httpResponseUrlNotFoundException);

            var expectedModelDependencyException =
                new ModelDependencyException(
                    invalidConfigurationModelException);

            this.openAiBrokerMock.Setup(broker => broker.GetAllModelsAsync())
                    .ThrowsAsync(httpResponseUrlNotFoundException);

            // when
            ValueTask<Model[]> getModelsTask =
               this.modelService.GetModelsAsync();

            ModelDependencyException actualModelException =
                await Assert.ThrowsAsync<ModelDependencyException>(
                    getModelsTask.AsTask);

            // then
            actualModelException.Should().BeEquivalentTo(
                expectedModelDependencyException);

            this.openAiBrokerMock.Verify(broker =>
                broker.GetAllModelsAsync(),
                        Times.Once);

            this.openAiBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnGetModelsIfTooManyRequestsOccurredAsync()
        {
            // given
            var httpResponseTooManyRequestsException =
                new HttpResponseTooManyRequestsException();

            var excessiveCallModelException =
                new ExcessiveCallModelException(
                    httpResponseTooManyRequestsException);

            var expectedModelDependencyValidationException =
                new ModelDependencyValidationException(
                    excessiveCallModelException);

            this.openAiBrokerMock.Setup(broker => broker.GetAllModelsAsync())
                    .ThrowsAsync(httpResponseTooManyRequestsException);

            // when
            ValueTask<Model[]> getModelsTask = this.modelService.GetModelsAsync();

            ModelDependencyValidationException actualModelDependencyValidationException =
                await Assert.ThrowsAsync<ModelDependencyValidationException>(
                    getModelsTask.AsTask);

            // then
            actualModelDependencyValidationException.Should().BeEquivalentTo(
                expectedModelDependencyValidationException);

            this.openAiBrokerMock.Verify(broker =>
                broker.GetAllModelsAsync(), Times.Once);

            this.openAiBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnGetModelsIfHttpErrorOccursAsync()
        {
            // given
            var httpResponseException =
                new HttpResponseException();

            var failedServerModelException =
                new FailedServerModelException(httpResponseException);

            var expectedModelDependencyException =
                new ModelDependencyException(
                    failedServerModelException);

            this.openAiBrokerMock.Setup(broker => broker.GetAllModelsAsync())
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<Model[]> getModelsTask =
               this.modelService.GetModelsAsync();

            ModelDependencyException actualModelException =
                await Assert.ThrowsAsync<ModelDependencyException>(
                    getModelsTask.AsTask);

            // then
            actualModelException.Should().BeEquivalentTo(
                expectedModelDependencyException);

            this.openAiBrokerMock.Verify(broker =>
                broker.GetAllModelsAsync(),
                        Times.Once);

            this.openAiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
