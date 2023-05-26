// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalFineTunes
{
    internal class ExternalFineTuneResponse
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("hyperparams")]
        public ExternalHyperParameters HyperParameters { get; set; }

        [JsonProperty("organization_id")]
        public string OrganizationId { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("training_files")]
        public ExternalTrainingFile[] TrainingFiles { get; set; }

        [JsonProperty("validation_files")]
        public object[] ValidationFiles { get; set; }

        [JsonProperty("result_files")]
        public object[] ResultFiles { get; set; }

        [JsonProperty("created_at")]
        public int CreatedDate { get; set; }

        [JsonProperty("updated_at")]
        public int UpdatedDate { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("fine_tuned_model")]
        public object FineTunedModel { get; set; }

        [JsonProperty("events")]
        public ExternalEvent[] Events { get; set; }
    }
}
