// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalFineTunes
{
    internal class ExternalHyperParameters
    {
        [JsonProperty("n_epochs")]
        public int EpochsCount { get; set; }

        [JsonProperty("batch_size")]
        public object BatchSize { get; set; }

        [JsonProperty("prompt_loss_weight")]
        public float PromptLossWeight { get; set; }

        [JsonProperty("learning_rate_multiplier")]
        public object LearningRateMultiplier { get; set; }
    }
}
