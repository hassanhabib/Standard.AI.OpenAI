// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

namespace Standard.AI.OpenAI.Models.Services.Foundations.FineTunes
{
    public class FineTuneRequest
    {
        public string FileId { get; set; }
        public string ValidationFile { get; set; }
        public string Model { get; set; }
        public string NumberOfDatasetCycles { get; set; }
        public int BatchSize { get; set; }
        public float LearningRateMultiplier { get; set; }
        public float PromptLossWeight { get; set; }
        public bool ComputeClassificationMetrics { get; set; }
        public int NumberOfClasses { get; set; }
        public string ClassificationPositiveClass { get; set; }
        public object[] ClassificationBetas { get; set; }
        public string Suffix { get; set; }
    }
}
