// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions;
using Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions.Exceptions;
using Xeptions;

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
                throw CreateAudioTranscriptionValidationException(
                    nullAudioTranscriptionException);
            }
            catch (InvalidAudioTranscriptionException invalidAudioTranscriptionException)
            {
                throw CreateAudioTranscriptionValidationException(
                    invalidAudioTranscriptionException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var invalidConfigurationAudioTranscriptionException =
                    new InvalidConfigurationAudioTranscriptionException(
                        message: "Invalid audio transcription configuration error occurred, contact support.",
                        httpResponseUrlNotFoundException);

                throw CreateAudioTranscriptionDependencyException(
                    invalidConfigurationAudioTranscriptionException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var unauthorizedAudioTranscriptionException =
                    new UnauthorizedAudioTranscriptionException(httpResponseUnauthorizedException);

                throw CreateAudioTranscriptionDependencyException(
                    unauthorizedAudioTranscriptionException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var unauthorizedAudioTranscriptionException =
                    new UnauthorizedAudioTranscriptionException(httpResponseForbiddenException);

                throw CreateAudioTranscriptionDependencyException(
                    unauthorizedAudioTranscriptionException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidAudioTranscriptionException =
                    new InvalidAudioTranscriptionException(
                        message: "Audio transcription is invalid.",
                        httpResponseBadRequestException);

                throw CreateAudioTranscriptionDependencyValidationException(
                    invalidAudioTranscriptionException);
            }
            catch (HttpResponseTooManyRequestsException httpResponseTooManyRequestsException)
            {
                var excessiveCallAudioTranscriptionException =
                    new ExcessiveCallAudioTranscriptionException(
                        message: "Excessive call error occurred, limit your calls.",
                        httpResponseTooManyRequestsException);

                throw CreateAudioTranscriptionDependencyValidationException(
                    excessiveCallAudioTranscriptionException);
            }
            catch (Exception exception)
            {
                var failedAudioTranscriptionServiceException =
                    new FailedAudioTranscriptionServiceException(
                        message: "Failed Audio Transcription Service Exception occurred, please contact support for assistance.", 
                        exception);

                throw new AudioTranscriptionServiceException(
                    message: "Audio transcription service error occurred, contact support.",
                    failedAudioTranscriptionServiceException);
            }
        }

        private static AudioTranscriptionValidationException CreateAudioTranscriptionValidationException(Xeption innerException)
        {
            return new AudioTranscriptionValidationException(
                message: "Audio transcription validation error occurred, fix errors and try again.",
                innerException);
        }

        private static AudioTranscriptionDependencyException CreateAudioTranscriptionDependencyException(Xeption innerException)
        {
            return new AudioTranscriptionDependencyException(
                message: "Audio transcription dependency error occurred, contact support.",
                innerException);
        }

        private static AudioTranscriptionDependencyValidationException CreateAudioTranscriptionDependencyValidationException(Xeption innerException)
        {
            return new AudioTranscriptionDependencyValidationException(
                message: "Chat completion dependency validation error occurred, fix errors and try again.",
                innerException);
        }












    }
}