// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

namespace Standard.AI.OpenAI.Models.Services.Foundations.FineTunes
{
    public class HyperParameter
    {
        public int EpochsCount { get; set; }
        public object BatchSize { get; set; }
        public float PromptLossWeight { get; set; }
        public object LearningRateMultiplier { get; set; }
    }
}
