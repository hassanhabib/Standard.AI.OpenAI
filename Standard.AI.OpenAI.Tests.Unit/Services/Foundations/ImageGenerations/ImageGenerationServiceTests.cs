// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalImageGenerations;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations;
using Standard.AI.OpenAI.Services.Foundations.ImageGenerations;
using Tynamix.ObjectFiller;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.ImageGenerations
{
    public partial class ImageGenerationServiceTests
    {
        private readonly Mock<IOpenAIBroker> openAIBrokerMock;
        private readonly ICompareLogic compareLogic;
        private readonly IImageGenerationService imageGenerationService;

        public ImageGenerationServiceTests()
        {
            this.openAIBrokerMock = new Mock<IOpenAIBroker>();
            this.compareLogic = new CompareLogic();

            this.imageGenerationService = new ImageGenerationService(
                openAIBroker: this.openAIBrokerMock.Object);
        }

        private static dynamic CreateRandomImageGenerationProperties()
        {
            return new
            {
                Prompt = GetRandomString(),
                ImagesToGenerate = GetRandomNumber(),
                ImageSize = GetRandomString(),
                ResponseFormat = GetRandomString(),
                User = GetRandomString(),
                Created = GetRandomNumber(),
                Results = GetRandomImageGenerationResults(),
            };
        }

        private Expression<Func<ExternalImageGenerationRequest, bool>> SameExternalImageGenerationRequestAs(
            ExternalImageGenerationRequest expectedExternalImageGenerationRequest)
        {
            return actualExternalImageGenerationRequest =>
                this.compareLogic.Compare(
                    expectedExternalImageGenerationRequest,
                    actualExternalImageGenerationRequest)
                        .AreEqual;
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static dynamic[] GetRandomImageGenerationResults()
        {
            return Enumerable.Range(start: 0, count: GetRandomNumber()).Select(
                item => new
                {
                    ImageUrl = GetRandomString(),
                    Base64EncodedJsonImage = GetRandomString()
                }).ToArray();
        }

        private static ImageGeneration CreateRandomImageGeneration() =>
            CreateImageGenerationFiller().Create();

        private static Filler<ImageGeneration> CreateImageGenerationFiller() =>
            new Filler<ImageGeneration>();
    }
}