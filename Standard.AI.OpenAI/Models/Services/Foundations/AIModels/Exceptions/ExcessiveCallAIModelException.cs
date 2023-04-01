// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIModels.Exceptions
{
    internal class ExcessiveCallAIModelException : Xeption
    {
        public ExcessiveCallAIModelException(Exception innerException)
            : base(message: "Excessive call error occurred, limit your calls.", innerException)
        { }
    }
}
