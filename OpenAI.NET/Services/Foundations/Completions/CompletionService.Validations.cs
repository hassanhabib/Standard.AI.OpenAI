// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using OpenAI.NET.Models.Completions;
using OpenAI.NET.Models.Completions.Exceptions;

namespace OpenAI.NET.Services.Foundations.Completions
{
    internal partial class CompletionService
    {
        private void ValidateCompletion(Completion completion)
        {
            ValidateCompletionNotNull(completion);
            ValidateCompletionRequestNotNull(completion.Request);

            Validate(
                (Rule: IsInvalid(completion.Request), Parameter: nameof(Completion.Request)),
                (Rule: IsInvalid(completion.Request.Model), Parameter: nameof(Completion.Request.Model)),
                (Rule: IsInvalid(completion.Request.Prompt), Parameter: nameof(Completion.Request.Prompt)));
        }

        private static void ValidateCompletionNotNull(Completion completion)
        {
            if (completion is null)
            {
                throw new NullCompletionException();
            }
        }

        private static void ValidateCompletionRequestNotNull(CompletionRequest request)
        {
            Validate(
                (Rule: IsInvalid(request), Parameter: nameof(Completion.Request)));
        }

        private static dynamic IsInvalid(object @object) => new
        {
            Condition = @object is null,
            Message = "Object is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Value is required"
        };

        private static dynamic IsInvalid(string[] textArray) => new
        {
            Condition = textArray is null || textArray.Length == 0,
            Message = "Value is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidCompletionException = new InvalidCompletionException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidCompletionException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidCompletionException.ThrowIfContainsErrors();
        }
    }
}
