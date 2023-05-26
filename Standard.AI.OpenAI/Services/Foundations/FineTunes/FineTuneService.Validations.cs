// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes;
using Standard.AI.OpenAI.Models.Services.Foundations.FineTunes.Exceptions;

namespace Standard.AI.OpenAI.Services.Foundations.FineTunes
{
    internal partial class FineTuneService
    {
        private static void ValidateFineTune(FineTune fineTune)
        {
            ValidateFineTuneNotNull(fineTune);

            Validate(
                (Rule: IsInvalid(fineTune.Request),
                Parameter: nameof(FineTune.Request)));

            Validate(
                (Rule: IsInvalid(fineTune.Request.FileId),
                Parameter: nameof(FineTuneRequest.FileId)));
        }

        private static void ValidateFineTuneNotNull(FineTune fineTune)
        {
            if (fineTune is null)
            {
                throw new NullFineTuneException();
            }
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidFineTuneException = new InvalidFineTuneException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidFineTuneException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidFineTuneException.ThrowIfContainsErrors();
        }

        private static dynamic IsInvalid(object @object) => new
        {
            Condition = @object is null,
            Message = "Value is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Value is required"
        };
    }
}
