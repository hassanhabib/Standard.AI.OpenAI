// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using OpenAI.NET.Models.Completions;
using OpenAI.NET.Models.Completions.Exceptions;
using System;
using System.ComponentModel.DataAnnotations;

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

            if (completion.Request is null)
            {
                var invalidCompletionException =
                    new InvalidCompletionException();

                invalidCompletionException.AddData(
                    nameof(completion.Request),
                    "Object is required");

                throw invalidCompletionException;
            }
        }
    }
}
