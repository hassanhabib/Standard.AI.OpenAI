// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;

namespace Standard.AI.OpenAI.Services.Foundations.Files
{
    internal interface IFileService
    {
        ValueTask<AIFile> RemoveFileByIdAsync(string fileId);
    }
}