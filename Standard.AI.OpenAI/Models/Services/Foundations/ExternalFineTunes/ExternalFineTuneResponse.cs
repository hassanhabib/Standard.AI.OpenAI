// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalFineTunes
{
    internal partial class ExternalFineTuneResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("created_at")]
        public int CreatedAt { get; set; }

        [JsonProperty("events")]
        public ExternalFineTuneEvent[] Events { get; set; }

        [JsonProperty("fine_tuned_model")]
        public string FineTunedModel { get; set; }

        [JsonProperty("hyperparams")]
        public ExternalFineTuneHyperParam HyperParams { get; set; }

        [JsonProperty("organization_id")]
        public string OrganizationId { get; set; }

        [JsonProperty("result_files")]
        public ExternalFineTuneResultFile[] ResultFiles { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("validation_files")]
        public ExternalFineTuneValidationFile[] ValidationFiles { get; set; }

        [JsonProperty("training_files")]
        public ExternalFineTuneTrainingFile[] TrainingFiles { get; set; }
        public int UpdatedAt { get; set; }
    }
}
