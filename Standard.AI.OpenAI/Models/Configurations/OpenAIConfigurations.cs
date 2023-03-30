// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

namespace Standard.AI.OpenAI.Models.Configurations
{
    public class OpenAIConfigurations
    {
        public string ApiUrl { get; set; } = "https://api.openai.com/v1";
        public string ApiKey { get; set; }
        public string OrganizationId { get; set; }
    }
}