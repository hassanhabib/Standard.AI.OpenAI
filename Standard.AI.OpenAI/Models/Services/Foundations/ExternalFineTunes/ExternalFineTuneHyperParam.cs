// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalFineTunes
{
    internal class ExternalFineTuneHyperParam
    {
        [JsonProperty("batch_size")]
        public int BatchSize { get; set; }

        [JsonProperty("learning_rate_multiplier")]
        public float LearningRateMultiplier { get; set; }

        [JsonProperty("n_epochs")]
        public int NumberOfEpochs { get; set; }

        [JsonProperty("prompt_loss_weight")]
        public float PromptLossWeight { get; set; }
    }   
}
