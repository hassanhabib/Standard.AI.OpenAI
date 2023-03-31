// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIModels
{
    public class AIModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string OwnedBy { get; set; }
        public AIModelPermission[] Permissions { get; set; }
        public string OriginModel { get; set; }
        public string Parent { get; set; }
    }
}
