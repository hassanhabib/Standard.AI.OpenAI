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
            ValidateCompletionNotNull(completion);

            Validate(
                (Rule: IsInvalid(completion.Request), Parameter: nameof(Completion.Request)));
        }

        private void ValidateCompletionNotNull(Completion completion)
        {
            if (completion is null)
            {
                throw new NullCompletionException();
            }
        }

        private dynamic IsInvalid(object @object) => new
        {
            Condition = @object is null,
            Message = "Object is required"
        };

        private void Validate(params (dynamic Rule, string Parameter)[] validations)
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
