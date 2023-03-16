// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using OpenAI.NET.Models.Completions;
using OpenAI.NET.Models.Completions.Exceptions;

namespace OpenAI.NET.Services.Foundations.Completions
{
    internal partial class CompletionService
    {
        private delegate ValueTask<Completion> ReturningCompletionFunction();

        private async ValueTask<Completion> TryCatch(ReturningCompletionFunction returningCompletionFunction)
        {
            try
            {
                return await returningCompletionFunction();
            }
            catch (NullCompletionException nullCompletionException)
            {
                throw new CompletionValidationException(nullCompletionException);
            }
        }
    }
}
