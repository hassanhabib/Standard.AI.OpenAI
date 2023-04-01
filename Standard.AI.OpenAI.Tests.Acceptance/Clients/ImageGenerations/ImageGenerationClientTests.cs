// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using Standard.AI.OpenAI.Clients.OpenAIs;
using Standard.AI.OpenAI.Models.Configurations;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalImageGenerations;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations;
using Tynamix.ObjectFiller;
using WireMock.Server;

namespace Standard.AI.OpenAI.Tests.Acceptance.Clients.ImageGenerations
{
    public partial class ImageGenerationClientTests : IDisposable
    {
        private readonly IOpenAIClient openAIClient;
        private readonly WireMockServer wireMockServer;
        private readonly string apiKey;
        private readonly string organizationId;

        public ImageGenerationClientTests()
        {
            this.wireMockServer = WireMockServer.Start();
            this.apiKey = CreateRandomString();
            this.organizationId = CreateRandomString();

            var openAIConfiguration = new OpenAIConfigurations
            {
                ApiUrl = this.wireMockServer.Url,
                ApiKey = this.apiKey,
                OrganizationId = this.organizationId
            };

            this.openAIClient = new OpenAIClient(openAIConfiguration);
        }

        private static ImageGeneration ConvertToImageGeneration(
            ImageGeneration imageGeneration,
            ExternalImageGenerationResponse externalImageGenerationResponse)
        {
            imageGeneration.Response = new ImageGenerationResponse
            {
                Created = DateTimeOffset.FromUnixTimeSeconds(externalImageGenerationResponse.Created),

                Results = externalImageGenerationResponse.Results.Select(result =>
                {
                    return new ImageGenerationResult
                    {
                        ImageUrl = result.ImageUrl,
                        Base64EncodedJsonImage = result.Base64EncodedJsonImage
                    };
                }).ToArray()
            };

            return imageGeneration;
        }

        private static ExternalImageGenerationResponse CreateRandomExternalImageGenerationResponse() =>
            CreateExternalImageGenerationResponseFiller().Create();

        private static ExternalImageGenerationRequest ConvertToImageGenerationRequest(ImageGeneration imageGeneration)
        {
            return new ExternalImageGenerationRequest
            {
                Prompt = imageGeneration.Request.Prompt,
                ImagesToGenerate = imageGeneration.Request.ImagesToGenerate,
                ImageSize = imageGeneration.Request.ImageSize,
                ResponseFormat = imageGeneration.Request.ResponseFormat,
                User = imageGeneration.Request.User
            };
        }

        private static ImageGeneration CreateRandomImageGeneration() =>
            CreateImageGenerationFiller().Create();

        private static DateTimeOffset CreateRandomDate() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static string CreateRandomString() =>
            new MnemonicString().GetValue();

        private static Filler<ImageGeneration> CreateImageGenerationFiller()
        {
            var filler = new Filler<ImageGeneration>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(CreateRandomDate);

            return filler;
        }

        private static Filler<ExternalImageGenerationResponse> CreateExternalImageGenerationResponseFiller() =>
            new Filler<ExternalImageGenerationResponse>();

        public void Dispose() => this.wireMockServer.Stop();
    }
}