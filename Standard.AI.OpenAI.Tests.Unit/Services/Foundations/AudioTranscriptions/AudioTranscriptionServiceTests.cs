// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.IO;
using System.Linq.Expressions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAudioTranscriptions;
using Standard.AI.OpenAI.Services.Foundations.AudioTranscriptions;
using Tynamix.ObjectFiller;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AudioTranscriptions
{
    public partial class AudioTranscriptionServiceTests
    {
        private readonly Mock<IOpenAIBroker> openAIBrokerMock;
        private readonly ICompareLogic compareLogic;
        private readonly IAudioTranscriptionService audioTranscriptionService;

        public AudioTranscriptionServiceTests()
        {
            this.openAIBrokerMock = new Mock<IOpenAIBroker>();
            this.compareLogic = new CompareLogic();

            this.audioTranscriptionService = new AudioTranscriptionService(
                openAIBroker: this.openAIBrokerMock.Object
            );
        }

        public static TheoryData UnAuthorizationExceptions()
        {
            return new TheoryData<HttpResponseException>
            {
                new HttpResponseUnauthorizedException(),
                new HttpResponseForbiddenException()
            };
        }

        private static dynamic CreateRandomAudioTranscriptionProperties()
        {
            Stream randomStream = CreateRandomStream();
            string randomFileName = CreateRandomString();
            string randomAudioTranscriptionModel = CreateRandomString();

            return new
            {
                ExternalFile = randomStream,
                Content = randomStream,
                FileName = randomFileName,
                Model = randomAudioTranscriptionModel,
                Prompt = CreateRandomString(),
                Temperature = CreateRandomDouble(),
                Language = CreateRandomString(),
                Text = CreateRandomString()
            };
        }

        private Expression<Func<ExternalAudioTranscriptionRequest, bool>> SameExternalAudioTranscriptionRequestAs(
            ExternalAudioTranscriptionRequest expectedExternalAudioTranscriptionRequest)
        {
            return actualExternalAudioTranscriptionRequest =>
                this.compareLogic.Compare(
                    expectedExternalAudioTranscriptionRequest,
                    actualExternalAudioTranscriptionRequest)
                        .AreEqual;
        }

        private static double CreateRandomDouble()
            => new SequenceGeneratorDouble().GetValue();

        private static string CreateRandomString()
            => new MnemonicString().GetValue();

        private static Stream CreateRandomStream()
        {
            var mockStream = new Mock<MemoryStream>();

            mockStream.SetupGet(stream =>
                stream.ReadTimeout)
                    .Returns(0);

            mockStream.SetupGet(stream =>
                stream.WriteTimeout)
                    .Returns(0);

            return mockStream.Object;
        }

        private static AudioTranscription CreateRandomAudioTranscription() =>
            AudioTranscriptionFiller().Create();

        private static Filler<AudioTranscription> AudioTranscriptionFiller()
        {
            var filler = new Filler<AudioTranscription>();

            filler.Setup()
                .OnType<Stream>().Use(CreateRandomStream);

            return filler;
        }
    }
}
