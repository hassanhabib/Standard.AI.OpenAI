// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Integration.APIs.AIModels
{
    public partial class AIModelsApiTests
    {
        [Fact(Skip = "This test is only for releases")]
        public async Task ShouldRetrieveAIModelByNameAsync()
        {
            // given
            string inputAIModelName = "text-davinci-003";

            // when
            AIModel retrievedAIModel =
                await this.openAIClient.AIModels.RetrieveAIModelByNameAsync(inputAIModelName);

            // then
            Assert.NotNull(retrievedAIModel);
        }
    }
}