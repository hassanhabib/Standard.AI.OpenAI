// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Integration.APIs.ImageGenerations
{
    public partial class ImageGenerationApiTests
    {
        [Fact(Skip = "This test is only for releases")]
        public async Task ShouldGenerateImageAsync()
        {
            // given
            var inputImageGeneration = new ImageGeneration
            {
                Request = new ImageGenerationRequest
                {
                    Prompt = "A cute baby sea otter"
                }
            };

            // when
            ImageGeneration responseImageGeneration =
                await this.openAIClient.ImageGenerations.GenerateImageAsync(
                    inputImageGeneration);

            // then
            Assert.NotNull(responseImageGeneration.Response);
        }
    }
}