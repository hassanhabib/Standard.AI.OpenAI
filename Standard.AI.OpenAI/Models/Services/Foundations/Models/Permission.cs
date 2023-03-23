// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

namespace Standard.AI.OpenAI.Models.Services.Foundations.Models
{
    public class Permission
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public int Created { get; set; }
        public bool AllowCreateEngine { get; set; }
        public bool AllowSampling { get; set; }
        public bool AllowLogprobs { get; set; }
        public bool AllowSearchIndices { get; set; }
        public bool AllowView { get; set; }
        public bool AllowFineTuning { get; set; }
        public string Organization { get; set; }
        public bool IsBlocking { get; set; }
    }
}
