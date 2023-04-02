// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Clients.AudioTranscriptions.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions.Exceptions;
using Standard.AI.OpenAI.Services.Foundations.AudioTranscriptions;
using Xeptions;

namespace Standard.AI.OpenAI.Clients.AudioTranscriptions
{
    internal class AudioTranscriptionsClient : IAudioTranscriptionsClient
    {
        private readonly IAudioTranscriptionService audioTranscriptionService;

        public AudioTranscriptionsClient(IAudioTranscriptionService audioTranscriptionService) =>
            this.audioTranscriptionService = audioTranscriptionService;

        public async ValueTask<AudioTranscription> SendAudioTranscriptionAsync(AudioTranscription audioTranscription)
        {
            try
            {
                return await this.audioTranscriptionService.SendAudioTranscriptionAsync(audioTranscription);
            }
            catch (AudioTranscriptionValidationException audioTransactionValidationException)
            {
                throw new AudioTranscriptionClientValidationException(
                    audioTransactionValidationException.InnerException as Xeption);
            }
            catch (AudioTranscriptionDependencyValidationException audioTransactionDependencyValidationException)
            {
                throw new AudioTranscriptionClientValidationException(
                    audioTransactionDependencyValidationException.InnerException as Xeption);
            }
            catch (AudioTranscriptionDependencyException audioTransactionDependencyException)
            {
                throw new AudioTranscriptionClientDependencyException(
                    audioTransactionDependencyException.InnerException as Xeption);
            }
            catch (AudioTranscriptionServiceException audioTransactionServiceException)
            {
                throw new AudioTranscriptionClientServiceException(
                    audioTransactionServiceException.InnerException as Xeption);
            }
        }
    }
}
