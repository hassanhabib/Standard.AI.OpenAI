// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIFiles
{
    public class AIFileResponse
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public int Size { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string Name { get; set; }
        public string Purpose { get; set; }
        public bool Deleted { get; set; }
        public AIFileStatus Status { get; set; }
        public string StatusDetails { get; set; }
    }
}
