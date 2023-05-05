// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.AIModels.Exceptions
{
    /// <summary>
    /// This exception is thrown when a validation error occurs while using the
    /// AI model client. For example, if required data is missing or invalid.
    /// </summary>
    public class AIModelClientValidationException : Xeption
    {
        public AIModelClientValidationException(Xeption innerException)
            : base(message: "AI model client validation error occurred, fix errors and try again.", innerException)
        { }
    }
}