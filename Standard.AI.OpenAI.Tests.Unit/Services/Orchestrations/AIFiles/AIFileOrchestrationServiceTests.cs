// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.LocalFiles.Exceptions;
using Standard.AI.OpenAI.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Services.Foundations.LocalFiles;
using Standard.AI.OpenAI.Services.Orchestrations.AIFiles;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Orchestrations.AIFiles
{
    public partial class AIFileOrchestrationServiceTests
    {
        private readonly Mock<ILocalFileService> localFileServiceMock;
        private readonly Mock<IAIFileService> aiFileServiceMock;
        private readonly ICompareLogic compareLogic;
        private readonly IAIFileOrchestrationService aiFileOrchestrationService;

        public AIFileOrchestrationServiceTests()
        {
            this.localFileServiceMock = new Mock<ILocalFileService>();
            this.aiFileServiceMock = new Mock<IAIFileService>();
            this.compareLogic = new CompareLogic();

            this.aiFileOrchestrationService = new AIFileOrchestrationService(
                localFileService: this.localFileServiceMock.Object,
                aiFileService: this.aiFileServiceMock.Object);
        }

        public static TheoryData DependencyValidationExceptions()
        {
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new LocalFileValidationException(someInnerException),
                new LocalFileDependencyValidationException(someInnerException),
                new AIFileValidationException(someInnerException),
                new AIFileDependencyValidationException(someInnerException)
            };
        }

        public static TheoryData DependencyExceptions()
        {
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new LocalFileDependencyException(someInnerException),
                new LocalFileServiceException(someInnerException),
                new AIFileDependencyException(someInnerException),
                new AIFileServiceException(someInnerException),
            };
        }

        public static TheoryData AIFileServiceDependencyExceptions()
        {
            var someInnerException = new Xeption();

            return new TheoryData<Xeption>
            {
                new AIFileDependencyException(someInnerException),
                new AIFileServiceException(someInnerException),
            };
        }

        private AIFile CreateRandomAIFile() =>
            CreateAIFileFiller().Create();

        private IEnumerable<AIFileResponse> CreateRandomAIFileResponses() =>
            CreateAIFileResponseFiller().Create(GetRandomNumber());

        private Expression<Func<AIFile, bool>> SameAIFileAs(
            AIFile expectedAIFile)
        {
            return actualAIFileRequest =>
                this.compareLogic.Compare(expectedAIFile, actualAIFileRequest)
                    .AreEqual;
        }

        private Expression<Func<string, bool>> SameFileIdAs(string fileId) => 
            actualFileId => actualFileId.Equals(fileId);

        private static string CreateRandomFilePath() =>
            new MnemonicString().GetValue();

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

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private Filler<AIFile> CreateAIFileFiller()
        {
            var filler = new Filler<AIFile>();

            filler.Setup()
                .OnType<DateTimeOffset>().IgnoreIt()
                .OnType<Stream>().Use(CreateRandomStream);

            return filler;
        }

        private Filler<AIFileResponse> CreateAIFileResponseFiller()
        {
            var filler = new Filler<AIFileResponse>();

            filler.Setup()
                .OnType<DateTimeOffset>().IgnoreIt();

            return filler;
        }

        private static string CreateRandomFileId()
        {
            var randomGuid = Guid.NewGuid();
            var randomId = $"file-{randomGuid.ToString("N").Substring(0, 22)}";
            
            return randomId;
        }
    }
}
