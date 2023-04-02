// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions.Exceptions;

namespace Standard.AI.OpenAI.Services.Foundations.AudioTranscriptions
{
    internal partial class AudioTranscriptionService : IAudioTranscriptionService
    {
        private delegate ValueTask<AudioTranscription> ReturningAudioTranscriptionFunction();

        private static async ValueTask<AudioTranscription> TryCatch(
            ReturningAudioTranscriptionFunction returningAudioTranscriptionFunction)
        {
            try
            {
                return await returningAudioTranscriptionFunction();
            }
            catch (NullAudioTranscriptionException nullAudioTranscriptionException)
            {
                throw new AudioTranscriptionValidationException(nullAudioTranscriptionException);
            }
            catch (InvalidAudioTranscriptionException invalidAudioTranscriptionException)
            {
                throw new AudioTranscriptionValidationException(invalidAudioTranscriptionException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var invalidConfigurationAudioTranscriptionException =
                    new InvalidConfigurationAudioTranscriptionException(httpResponseUrlNotFoundException);

                throw new AudioTranscriptionDependencyException(invalidConfigurationAudioTranscriptionException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedAudioTranscriptionException =
                    new UnauthorizedAudioTranscriptionException(httpResponseUnauthorizedException);

                throw new AudioTranscriptionDependencyException(unauthorizedAudioTranscriptionException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedAudioTranscriptionException =
                    new UnauthorizedAudioTranscriptionException(httpResponseForbiddenException);

                throw new AudioTranscriptionDependencyException(unauthorizedAudioTranscriptionException);
            }
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundAudioTranscriptionException =
                    new NotFoundAudioTranscriptionException(httpResponseNotFoundException);

                throw new AudioTranscriptionDependencyValidationException(notFoundAudioTranscriptionException);
            }
            catch (HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallAudioTranscriptionException =
                    new ExcessiveCallAudioTranscriptionException(httpResponseTooManyRequestsException);

                throw new AudioTranscriptionDependencyValidationException(excessiveCallAudioTranscriptionException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedServerAudioTranscriptionException =
                    new FailedServerAudioTranscriptionException(httpResponseException);

                throw new AudioTranscriptionDependencyException(failedServerAudioTranscriptionException);
            }
            catch (Exception exception)
            {
                var failedAudioTranscriptionServiceException =
                    new FailedAudioTranscriptionServiceException(exception);

                throw new AudioTranscriptionServiceException(failedAudioTranscriptionServiceException);
            }
        }
    }
}