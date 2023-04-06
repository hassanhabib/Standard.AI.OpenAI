// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.Files;
using Standard.AI.OpenAI.Models.Services.Foundations.Files.Exceptions;

namespace Standard.AI.OpenAI.Services.Foundations.Files
{
    internal partial class FileService
    {
        private delegate ValueTask<File> ReturningFileFunction();

        private async ValueTask<File> TryCatch(ReturningFileFunction returningFileFunction)
        {
            try
            {
                return await returningFileFunction();
            }
            catch (InvalidFileException invalidFileException)
            {
                throw new FileValidationException(invalidFileException);
            }
        }
    }
}