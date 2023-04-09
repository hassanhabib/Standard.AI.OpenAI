// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalFineTunes
{
    internal class ExternalFineTuneRequest
    {
        [JsonProperty("training_file")]
        public string TrainingFile { get; set; }

        [JsonProperty("validation_file")]
        public string ValidationFile { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("n_epochs")]
        public int NumberOfEpochs { get; set; }

        [JsonProperty("batch_size")]
        public int BatchSize { get; set; }

        [JsonProperty("learning_rate_multiplier")]
        public double LearningRateMultiplier { get; set; }

        [JsonProperty("prompt_loss_weight")]
        public double PromptLossWeight { get; set; }

        [JsonProperty("compute_classification_metrics")]
        public bool ComputeClassificationMetrcs { get; set; }

        [JsonProperty("classification_n_classes")]
        public int NumberOfClasses { get; set; }

        [JsonProperty("classification_positive_class")]
        public string PositiveClass { get; set; }

        [JsonProperty("classification_betas")]
        public int[] ClassificatonBetas { get; set; }

        [JsonProperty("suffix")]
        public string Suffix { get; set; }
    }
}