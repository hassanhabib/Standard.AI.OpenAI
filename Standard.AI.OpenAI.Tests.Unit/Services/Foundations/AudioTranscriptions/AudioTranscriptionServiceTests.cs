// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.IO;
using System.Linq.Expressions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAudioTranscriptions;
using Standard.AI.OpenAI.Services.Foundations.AudioTranscriptions;
using Tynamix.ObjectFiller;

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

        private static dynamic CreateRandomAudioTranscriptionProperties()
        {
            Stream randomStream = CreateRandomStream();
            string randomFileName = CreateRandomString();
            AudioTranscriptionModel randomAudioTranscriptionModel = CreateRandomAudioTranscriptionModel();

            return new
            {
                ExternalFile = randomStream,
                Content = randomStream,
                FileName = randomFileName,
                Model = randomAudioTranscriptionModel,
                Prompt = CreateRandomString(),
                Temperature = CreateRandomDecimal(),
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

        private static decimal CreateRandomDecimal()
            => new SequenceGeneratorDecimal().GetValue();

        private static string CreateRandomString()
            => new MnemonicString().GetValue();

        private static AudioTranscriptionModel CreateRandomAudioTranscriptionModel()
        {
            string randomString = CreateRandomString();
            return AudioTranscriptionModel.Create(randomString);
        }

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

            filler.Setup()
                .OnType<AudioTranscriptionModel>()
                .Use(new AudioTranscriptionModel[]
                {
                    AudioTranscriptionModel.Create(CreateRandomString()),
                    AudioTranscriptionModel.Create(CreateRandomString()),
                    AudioTranscriptionModel.Create(CreateRandomString())
                });

            return filler;
        }
    }
}
