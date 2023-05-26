// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Xeptions;

namespace Standard.AI.OpenAI.Models.Services.Foundations.FineTunes.Exceptions
{
    public class UnauthorizedFineTuneException : Xeption
    {
        public UnauthorizedFineTuneException(Exception innerException)
            : base(message: "Unauthorized fine tune request, fix errors and try again.",
                innerException)
        { }
    }
}
