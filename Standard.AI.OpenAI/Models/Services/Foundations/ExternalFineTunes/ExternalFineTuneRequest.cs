// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalFineTunes
{
    internal class ExternalFineTuneRequest
    {
        [JsonProperty("training_file")]
        public string FileId { get; set; }

        [JsonProperty("validation_file")]
        public string ValidationFile { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }
        
        [JsonProperty("n_epochs")]
        public string NumberOfDatasetCycles { get; set; }

        [JsonProperty("batch_size")]
        public int BatchSize { get; set; }

        [JsonProperty("learning_rate_multiplier")]
        public float LearningRateMultiplier { get; set; }

        [JsonProperty("prompt_loss_weight")]
        public float PromptLossWeight { get; set; }
        
        [JsonProperty("compute_classification_metrics")]
        public bool ComputeClassificationMetrics { get; set; }

        [JsonProperty("classification_n_classes")]
        public int NumberOfClasses { get; set; }

        [JsonProperty("classification_positive_class")]
        public string ClassificationPositiveClass { get; set; }

        [JsonProperty("classification_betas")]
        public object[] ClassificationBetas { get; set; }

        [JsonProperty("suffix")]
        public string Suffix { get; set; }
    }
}
