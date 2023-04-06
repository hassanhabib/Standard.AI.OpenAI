// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.Files;

namespace Standard.AI.OpenAI.Services.Foundations.Files
{
    internal interface IFileService
    {
        ValueTask<File> RemoveFileByIdAsync(string fileId);
    }
}