// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.IO;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIFiles
{
    internal class AIFileRequest
    {
        public string Name { get; set; }
        public Stream Content { get; set; }
        public string Purpose { get; set; }
    }
}
