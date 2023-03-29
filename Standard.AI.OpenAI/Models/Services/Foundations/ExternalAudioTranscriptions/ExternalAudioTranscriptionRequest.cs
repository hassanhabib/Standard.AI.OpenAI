// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Standard.AI.OpenAI.Models.Services.Foundations.ExternalAudioTranscriptions
{
    internal class ExternalAudioTranscriptionRequest
    {
        /// <summary>
        /// The path to the audio file to transcribe, in one of these formats: mp3, mp4, mpeg, mpga, m4a, wav, or webm.
        /// </summary>
        [JsonIgnore]
        public string FilePath { get; set; }

        /// <summary>
        /// ID of the model to use. Only whisper-1 is currently available.
        /// </summary>
        [JsonProperty(propertyName: "model")]
        public string Model { get; set; }

        /// <summary>
        /// An optional text to guide the model's style or continue a previous audio segment.
        /// The prompt should match the audio language.
        /// </summary>
        [JsonProperty(propertyName: "prompt")]
        public string Prompt { get; set; } = null;

        /// <summary>
        /// The sampling temperature, between 0 and 1.
        /// Higher values like 0.8 will make the output more random,
        /// while lower values like 0.2 will make it more focused and deterministic.
        /// If set to 0, the model will use log probability to automatically increase
        /// the temperature until certain thresholds are hit.
        /// </summary>
        [JsonProperty(propertyName: "temperature")]
        public decimal Temperature { get; set; } = 0.0M;

        /// <summary>
        /// The language of the input audio. Supplying the input language in ISO-639-1
        /// format will improve accuracy and latency.
        /// </summary>
        [JsonProperty(propertyName: "language")]
        public string Language { get; set; } = null;
    }
}
