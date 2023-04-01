// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIModels
{
    public class AIModelPermission
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public bool AllowCreateEngine { get; set; }
        public bool AllowSampling { get; set; }
        public bool AllowLogProbabilities { get; set; }
        public bool AllowSearchIndices { get; set; }
        public bool AllowView { get; set; }
        public bool AllowFineTuning { get; set; }
        public string Organization { get; set; }
        public bool IsBlocking { get; set; }
    }
}
