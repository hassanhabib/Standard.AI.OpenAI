// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions;

namespace Standard.AI.OpenAI.Clients.AudioTranscriptions
{
    public interface IAudioTranscriptionsClient
    {
        ValueTask<AudioTranscription> SendAudioTranscriptionAsync(AudioTranscription audioTranscription);
    }
}
