// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions.Exceptions;

namespace Standard.AI.OpenAI.Services.Foundations.ChatCompletions
{
    internal partial class ChatCompletionService
    {
        private void ValidateChatCompletionOnSend(ChatCompletion chatCompletion)
        {
            ValidateChatCompletionIsNotNull(chatCompletion);

            Validate(
                (Rule: IsInvalid(chatCompletion.Request),
                Parameter: nameof(ChatCompletion.Request)));

            Validate(
                (Rule: IsInvalid(chatCompletion.Request.Messages),
                Parameter: nameof(ChatCompletionRequest.Messages)),

                (Rule: IsInvalid(chatCompletion.Request.Model),
                Parameter: nameof(ChatCompletionRequest.Model)));
        }

        private void ValidateChatCompletionIsNotNull(ChatCompletion chatCompletion)
        {
            if (chatCompletion is null)
            {
                throw new NullChatCompletionException();
            }
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

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidChatCompletionException = new InvalidChatCompletionException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidChatCompletionException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidChatCompletionException.ThrowIfContainsErrors();
        }
    }
}