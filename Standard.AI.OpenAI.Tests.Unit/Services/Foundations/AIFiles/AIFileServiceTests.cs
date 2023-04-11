// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.IO;
using System.Linq.Expressions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Standard.AI.OpenAI.Brokers.DateTimes;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Services.Foundations.AIFiles;
using Tynamix.ObjectFiller;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AIFiles
{
    public partial class AIFileServiceTests
    {
        private readonly Mock<IOpenAIBroker> openAiBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly ICompareLogic compareLogic;
        private readonly IAIFileService aiFileService;

        public AIFileServiceTests()
        {
            this.openAiBrokerMock = new Mock<IOpenAIBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.compareLogic = new CompareLogic();

            this.aiFileService = new AIFileService(
                openAIBroker: this.openAiBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object);
        }

        public dynamic CreateRandomFileProperties(int created, DateTimeOffset createdDate)
        {
            Stream randomStream = CreateRandomStream();
            string randomFileName = CreateRandomString();
            string randomFileType = CreateRandomString();
            int randomBytesSize = GetRandomNumber();

            return new
            {
                ExternalFile = randomStream,
                Content = randomStream,
                FileName = randomFileName,
                Name = randomFileName,
                Type = randomFileType,
                Object = randomFileType,
                Purpose = CreateRandomString(),
                Id = CreateRandomString(),
                Size = randomBytesSize,
                Bytes = randomBytesSize,
                Created = created,
                CreatedDate = createdDate
            };
        }

        private Expression<Func<ExternalAIFileRequest, bool>> SameExternalAIFileRequestAs(
            ExternalAIFileRequest expectedExternalAIFileRequest)
        {
            return actualExternalAIFileRequest =>
                this.compareLogic.Compare(
                    expectedExternalAIFileRequest,
                    actualExternalAIFileRequest)
                        .AreEqual;
        }

        private static DateTimeOffset CreateRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static string CreateRandomString() =>
            new MnemonicString().GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private Stream CreateRandomStream()
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
    }
}
