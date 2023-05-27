// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Clients.OpenAIs;
using Standard.AI.OpenAI.Models.Configurations;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;

namespace Standard.AI.OpenAI.Tests.Integration.APIs.FineTunes
{
    public partial class FineTuneTests
    {
        private readonly IOpenAIClient openAIClient;

        public FineTuneTests()
        {
            var openAIConfigurations = new OpenAIConfigurations
            {
                ApiKey = Environment.GetEnvironmentVariable("ApiKey"),
                OrganizationId = Environment.GetEnvironmentVariable("OrgId"),
                ApiUrl = "https://api.openai.com/"
            };

            this.openAIClient = new OpenAIClient(openAIConfigurations);
        }

        private async ValueTask<AIFile> SubmitRandomFileAsync()
        {
            MemoryStream memoryStream = CreateRandomStream();

            var aiFile = new AIFile
            {
                Request = new AIFileRequest
                {
                    Name = "Test",
                    Content = memoryStream,
                    Purpose = "fine-tune"
                }
            };

            return await this.openAIClient.AIFiles
                .UploadFileAsync(aiFile);
        }

        private static MemoryStream CreateRandomStream()
        {
            string content = "{\"prompt\": \"<prompt text>\", \"completion\": \"<ideal generated text>\"}";

            return new MemoryStream(Encoding.UTF8.GetBytes(content));
        }
    }
}
