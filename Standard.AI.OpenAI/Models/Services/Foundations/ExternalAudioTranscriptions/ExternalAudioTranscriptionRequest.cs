// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.IO;
using RESTFulSense.Models.Attributes;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalAudioTranscriptions
{
    internal class ExternalAudioTranscriptionRequest
    {
        [RESTFulStreamContent(name: "file")]
        public Stream File { get; set; }

        [RESTFulFileName(name: "file")]
        public string FileName { get; set; }

        [RESTFulStringContent(name: "model")]
        public string Model { get; set; }

        [RESTFulStringContent(name: "prompt")]
        public string Prompt { get; set; }

        [RESTFulStringContent(name: "temperature")]
        public double Temperature { get; set; }

        [RESTFulStringContent(name: "language")]
        public string Language { get; set; }
    }
}
