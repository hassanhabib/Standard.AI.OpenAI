// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

namespace Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions
{
    public class AudioTranscriptionRequest
    {
        /// <summary>
        /// The path to the audio file to transcribe, in one of these formats: mp3, mp4, mpeg, mpga, m4a, wav, or webm.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// ID of the model to use. Only whisper-1 is currently available.
        /// </summary>
        public AudioTranscriptionModel Model { get; set; }

        /// <summary>
        /// The format of the transcript output, in one of these options: json, text, srt, verbose_json, or vtt.
        /// </summary>
        public string ResponseFormat { get; set; }

        /// <summary>
        /// An optional text to guide the model's style or continue a previous audio segment.
        /// The prompt should match the audio language.
        /// </summary>
        public string Prompt { get; set; } = null;

        /// <summary>
        /// The sampling temperature, between 0 and 1.
        /// Higher values like 0.8 will make the output more random,
        /// while lower values like 0.2 will make it more focused and deterministic.
        /// If set to 0, the model will use log probability to automatically increase
        /// the temperature until certain thresholds are hit.
        /// </summary>
        public decimal Temperature { get; set; } = 0.0M;

        /// <summary>
        /// The language of the input audio. Supplying the input language in ISO-639-1
        /// format will improve accuracy and latency.
        /// </summary>
        public string Language { get; set; } = null;
    }
}