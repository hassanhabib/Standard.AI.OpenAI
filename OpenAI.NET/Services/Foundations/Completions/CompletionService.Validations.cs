// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using OpenAI.NET.Models.Completions;
using OpenAI.NET.Models.Completions.Exceptions;

namespace OpenAI.NET.Services.Foundations.Completions
{
    internal partial class CompletionService
    {
        private void ValidateCompletion(Completion completion)
        {
            if (completion is null)
            {
                throw new NullCompletionException();
            }
        }
    }
}
