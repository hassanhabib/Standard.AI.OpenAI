// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations;
using Standard.AI.OpenAI.Models.Services.Foundations.ImageGenerations.Exceptions;

namespace Standard.AI.OpenAI.Services.Foundations.ImageGenerations
{
    internal partial class ImageGenerationService
    {
        private static void ValidateImageGenerationOnGenerate(ImageGeneration imageGeneration)
        {
            ValidateImageGenerationIsNotNull(imageGeneration);

            Validate(
                (Rule: IsInvalid(imageGeneration.Request),
                Parameter: nameof(ImageGeneration.Request)));

            Validate(
                (Rule: IsInvalid(imageGeneration.Request.Prompt),
                Parameter: nameof(ImageGeneration.Request.Prompt)));
        }

        private static void ValidateImageGenerationIsNotNull(ImageGeneration imageGeneration)
        {
            if (imageGeneration is null)
            {
                throw new NullImageGenerationException(
                    message: "Image generation is null.");
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
            var invalidImageGenerationException = 
                new InvalidImageGenerationException(
                    message: "Invalid image generation error occurred, fix errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidImageGenerationException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidImageGenerationException.ThrowIfContainsErrors();
        }
    }
}