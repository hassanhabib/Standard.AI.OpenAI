// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.IO;
using RESTFulSense.Models.Attributes;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalAIFiles
{
    internal class ExternalAIFileRequest
    {
        [RESTFulStreamContent("file")]
        public Stream File { get; set; }

        [RESTFulFileName("file")]
        public string FileName { get; set; }

        [RESTFulStringContent("purpose")]
        public string Purpose { get; set; }
    }
}
