// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Clients.ImageGenerations.Exceptions
{
    public class ImageGenerationClientDependencyException : Xeption
    {
        public ImageGenerationClientDependencyException(Xeption innerException)
            : base(message: "Image generation client dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
