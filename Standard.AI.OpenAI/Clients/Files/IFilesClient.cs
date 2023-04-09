﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.Files;

namespace Standard.AI.OpenAI.Clients.Files
{
    public interface IFilesClient
    {
        ValueTask<File> RemoveFileByIdAsync(string fileId);
    }
}