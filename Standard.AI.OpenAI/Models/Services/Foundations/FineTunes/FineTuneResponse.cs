// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;

namespace Standard.AI.OpenAI.Models.Services.Foundations.FineTunes
{
    public class FineTuneResponse
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public HyperParameter HyperParameters { get; set; }
        public string OrganizationId { get; set; }
        public string Model { get; set; }
        public TrainingFile[] TrainingFiles { get; set; }
        public object[] ValidationFiles { get; set; }
        public object[] ResultFiles { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public string Status { get; set; }
        public object FineTunedModel { get; set; }
        public Event[] Events { get; set; }
    }
}
