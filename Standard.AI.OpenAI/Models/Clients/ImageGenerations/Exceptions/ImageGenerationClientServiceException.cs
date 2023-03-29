// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.ImageGenerations.Exceptions
{
    public class ImageGenerationClientServiceException : Xeption
    {
        public ImageGenerationClientServiceException(Xeption innerException)
            : base(message: "Image generation client service error occurred, contact support.",
                  innerException)
        { }
    }
}