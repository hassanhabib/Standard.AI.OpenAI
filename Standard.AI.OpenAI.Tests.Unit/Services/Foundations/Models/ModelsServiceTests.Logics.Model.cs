// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalModels;
using Standard.AI.OpenAI.Models.Services.Foundations.Models;
using Tynamix.ObjectFiller;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.Models
{
    public partial class ModelsServiceTests
    {
        [Fact]
        public async Task ShouldGetModelsAsync()
        {
            ExternalModelsResult randomExternalModelsResult = CreateRandomExternalModelsResult();

            ExternalModelsResult expectedExternalModelsResult = randomExternalModelsResult;

            // given
            this.openAiBrokerMock.Setup(broker => broker.GetAllModelsAsync())
                .ReturnsAsync(expectedExternalModelsResult);

            // when
            Model[] actualModels = await this.modelService.GetModelsAsync();


            // then
            this.openAiBrokerMock.Verify(broker => broker.GetAllModelsAsync(), Times.Once);


            this.openAiBrokerMock.VerifyNoOtherCalls();
        }

    }
}