// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Standard.AI.OpenAI.Clients.OpenAIs;
using Standard.AI.OpenAI.Models.Configurations;

namespace Standard.AI.OpenAI.Tests.Integration.APIs.AIFiles
{
    public partial class AIFilesTests
    {
        private readonly IOpenAIClient openAIClient;

        public AIFilesTests()
        {
            IConfiguration config = new ConfigurationBuilder().AddEnvironmentVariables().Build();

            OpenAIConfigurations openAIConfigurations =
                config.GetSection(key: "OpenAI").Get<OpenAIConfigurations>();

            this.openAIClient = new OpenAIClient(openAIConfigurations);
        }

        private static MemoryStream CreateRandomStream()
        {
            string content = "{\"prompt\": \"<prompt text>\", \"completion\": \"<ideal generated text>\"}";

            return new MemoryStream(Encoding.UTF8.GetBytes(content));
        }
    }
}
