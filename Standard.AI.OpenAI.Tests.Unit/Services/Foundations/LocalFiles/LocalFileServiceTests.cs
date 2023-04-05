// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Standard.AI.OpenAI.Brokers.Files;
using Standard.AI.OpenAI.Services.Foundations.LocalFiles;
using Tynamix.ObjectFiller;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.LocalFiles
{
    public partial class LocalFileServiceTests
    {
        private readonly Mock<IFileBroker> fileBrokerMock;
        private readonly ILocalFileService localFileService;

        public LocalFileServiceTests()
        {
            this.fileBrokerMock = new Mock<IFileBroker>();
            
            this.localFileService = new LocalFileService(
                fileBroker: this.fileBrokerMock.Object);
        }

        private Stream CreateRandomStream()
        {
            
        }
    }
}
