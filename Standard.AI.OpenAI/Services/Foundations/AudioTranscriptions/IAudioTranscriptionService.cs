// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions;

namespace Standard.AI.OpenAI.Services.Foundations.AudioTranscriptions
{
    internal interface IAudioTranscriptionService
    {
        ValueTask<AudioTranscription> SendAudioTranscriptionAsync(AudioTranscription audioTranscription);
    }
}
