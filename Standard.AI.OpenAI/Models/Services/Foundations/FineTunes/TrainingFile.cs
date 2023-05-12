// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;

namespace Standard.AI.OpenAI.Models.Services.Foundations.FineTunes
{
    public class TrainingFile
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Purpose { get; set; }
        public string Filename { get; set; }
        public int Bytes { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string Status { get; set; }
        public object StatusDetails { get; set; }
    }
}
