// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalAudioTranscriptions
{
    internal class ExternalAudioTranscriptionResponse
    {
        [JsonProperty(propertyName: "text")]
        public string Text { get; set; } = string.Empty;
    }
}
