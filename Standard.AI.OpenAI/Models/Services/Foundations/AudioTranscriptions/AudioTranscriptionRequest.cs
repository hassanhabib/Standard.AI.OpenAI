// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.IO;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions
{
    public class AudioTranscriptionRequest
    {
        /// <summary>
        /// The audio file to transcribe, in one of these formats: mp3, mp4,
        /// mpeg, mpga, m4a, wav, or webm.
        /// </summary>
        public Stream Content { get; set; }

        /// <summary>
        /// The file name of the audio file to transcribe.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// ID of the model to use. Only whisper-1 is currently available.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// An optional text to guide the model's style or continue a previous
        /// audio segment. The prompt should match the audio language.
        /// </summary>
        public string Prompt { get; set; }

        /// <summary>
        /// The sampling temperature, between 0 and 1. Higher values like 0.8
        /// will make the output more random, while lower values like 0.2 will
        /// make it more focused and deterministic. If set to 0, the model will
        /// use log probability to automatically increase the temperature until
        /// certain thresholds are hit.
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// The language of the input audio. Supplying the input language in
        /// ISO-639-1 format will improve accuracy and latency.
        /// </summary>
        public string Language { get; set; }
    }
}