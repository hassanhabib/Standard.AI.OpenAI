// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AIModels
{
    public partial class AIModelServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveIfUrlNotFoundAsync()
        {
            var httpResponseUrlNotFoundException =
                new HttpResponseUrlNotFoundException();

            var invalidConfigurationAIModelException =
                new InvalidConfigurationAIModelException(
                    httpResponseUrlNotFoundException);

            var expectedAIModelDependencyException =
                new AIModelDependencyException(
                    invalidConfigurationAIModelException);

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAllAIModelsAsync())
                        .ThrowsAsync(httpResponseUrlNotFoundException);

            // when
            ValueTask<IEnumerable<AIModel>> getAllAIModelsTask =
               this.aiModelService.RetrieveAllAIModelsAsync();

            AIModelDependencyException
                actualAIModelDependencyException =
                    await Assert.ThrowsAsync<AIModelDependencyException>(
                        getAllAIModelsTask.AsTask);

            // then
            actualAIModelDependencyException.Should().BeEquivalentTo(
                expectedAIModelDependencyException);

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAllAIModelsAsync(),
                        Times.Once);

            this.openAIBrokerMock.VerifyNoOtherCalls();
        }
    }
}
