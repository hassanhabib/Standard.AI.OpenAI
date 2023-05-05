// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIFiles.Exceptions
{
    public partial class NullAIFileException : Xeption
    {
        public NullAIFileException()
            : base(message: "Ai file is null.")
        { }
    }
}
