// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Brokers.DateTimes;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalImageGenerations;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations;
using Standard.AI.OpenAI.Services.Foundations.ImageGenerations;
using Tynamix.ObjectFiller;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.ImageGenerations
{
    public partial class ImageGenerationServiceTests
    {
        private readonly Mock<IOpenAIBroker> openAIBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly ICompareLogic compareLogic;
        private readonly IImageGenerationService imageGenerationService;

        public ImageGenerationServiceTests()
        {
            this.openAIBrokerMock = new Mock<IOpenAIBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.compareLogic = new CompareLogic();

            this.imageGenerationService = new ImageGenerationService(
                openAIBroker: this.openAIBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object);
        }

        private static dynamic CreateRandomImageGenerationProperties(
            DateTimeOffset createdDate,
            int createdDateNumber)
        {
            return new
            {
                Prompt = GetRandomString(),
                ImagesToGenerate = GetRandomNumber(),
                ImageSize = GetRandomString(),
                ResponseFormat = GetRandomString(),
                User = GetRandomString(),
                Created = createdDateNumber,
                CreatedDate = createdDate,
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

        private static int GetRandomDateNumber() =>
            new Random((int)Stopwatch.GetTimestamp()).Next(int.MinValue, int.MaxValue);

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

        private static DateTimeOffset GetRandomDate() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<ImageGeneration> CreateImageGenerationFiller()
        {
            var filler = new Filler<ImageGeneration>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDate());

            return filler;
        }

        public static TheoryData UnauthorizedExceptions()
        {
            return new TheoryData<HttpResponseException>
            {
                new HttpResponseUnauthorizedException(),
                new HttpResponseForbiddenException()
            };
        }
    }
}