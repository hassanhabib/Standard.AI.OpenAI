// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Newtonsoft.Json;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalImageGenerations;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Acceptance.Clients.ImageGenerations
{
    public partial class ImageGenerationClientTests
    {
        [Fact]
        public async Task ShouldGenerateImageAsync()
        {
            // given
            ImageGeneration randomImageGeneration = CreateRandomImageGeneration();
            ImageGeneration inputImageGeneration = randomImageGeneration;

            ExternalImageGenerationRequest imageGenerationRequest =
                ConvertToImageGenerationRequest(inputImageGeneration);

            ExternalImageGenerationResponse imageGenerationResponse =
                CreateRandomExternalImageGenerationResponse();

            ImageGeneration expectedImageGeneration = inputImageGeneration.DeepClone();
            expectedImageGeneration = ConvertToImageGeneration(expectedImageGeneration, imageGenerationResponse);

            var jsonSerializationSettings = new JsonSerializerSettings();
            jsonSerializationSettings.DefaultValueHandling = DefaultValueHandling.Ignore;

            this.wireMockServer.Given(
                Request.Create()
                .UsingPost()
                    .WithPath("/v1/images/generations")
                    .WithHeader("Authorization", $"Bearer {this.apiKey}")
                    .WithHeader("OpenAI-Organization", this.organizationId)
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithBody(JsonConvert.SerializeObject(
                        imageGenerationRequest,
                        jsonSerializationSettings)))
                .RespondWith(
                    Response.Create()
                    .WithBodyAsJson(imageGenerationResponse));

            // when
            ImageGeneration actualImageGeneration =
                await this.openAIClient.ImageGenerations.GenerateImageAsync(
                    inputImageGeneration);

            // then
            actualImageGeneration.Should().BeEquivalentTo(expectedImageGeneration);
        }
    }
}