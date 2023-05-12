// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.AudioTranscriptions.Exceptions
{
    public class AudioTranscriptionClientValidationException : Xeption
    {
        public AudioTranscriptionClientValidationException(Xeption innerException)
            : base(
                message: "Audio transcription client validation error occurred, fix errors and try again.",
                innerException)
        { }
    }
}
