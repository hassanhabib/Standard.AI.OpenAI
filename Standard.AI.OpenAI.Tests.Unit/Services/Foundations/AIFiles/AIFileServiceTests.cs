// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Moq;
using Standard.AI.OpenAI.Brokers.DateTimes;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Services.Foundations.AIFiles;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AIFiles
{
    public partial class AIFileServiceTests
    {
        private readonly Mock<IOpenAIBroker> openAiBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly IAIFileService aiFileService;

        public AIFileServiceTests()
        {
            this.openAiBrokerMock = new Mock<IOpenAIBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();

            this.aiFileService = new AIFileService(
                openAIBroker: this.openAiBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object);
        }

        public dynamic CreateRandomFileProperties()
        {
            Stream 

            return new
            {
                ExternalFile = CreateRandomStreamContent()
            };
        }
    }
}
