// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.AudioTranscriptions.Exceptions
{
#pragma warning disable RCS1194 // Implement exception constructors.
    public class AudioTranscriptionClientServiceException : Xeption
    {
        public AudioTranscriptionClientServiceException(Xeption innerException)
            : base(
                message: "Audio transcription client service error occurred, contact support.",
                innerException)
        { }
    }
}
