// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.Files;
using Standard.AI.OpenAI.Models.Services.Foundations.Files.Exceptions;
using Standard.AI.OpenAI.Services.Foundations.Files;
using Xeptions;

namespace Standard.AI.OpenAI.Clients.Files
{
    internal class FilesClient : IFilesClient
    {
        private readonly IFileService fileService;

        public FilesClient(IFileService fileService) =>
            this.fileService = fileService;

        public async ValueTask<File> RemoveFileByIdAsync(string fileId)
        {
            try
            {
                return await this.fileService.RemoveFileByIdAsync(fileId);
            }
            catch (FileValidationException fileValidationException)
            {
                throw new FileValidationException(
                    fileValidationException.InnerException as Xeption);
            }
            catch (FileDependencyValidationException fileDependencyValidationException)
            {
                throw new FileValidationException(
                    fileDependencyValidationException.InnerException as Xeption);
            }
            catch (FileDependencyException fileDependencyException)
            {
                throw new FileDependencyException(
                    fileDependencyException.InnerException as Xeption);
            }
            catch (FileServiceException fileServiceException)
            {
                throw new FileServiceException(
                    fileServiceException.InnerException as Xeption);
            }
        }
    }
}