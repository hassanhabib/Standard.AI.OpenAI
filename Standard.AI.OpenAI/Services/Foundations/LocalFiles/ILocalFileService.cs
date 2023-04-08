// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.IO;

namespace Standard.AI.OpenAI.Services.Foundations.LocalFiles
{
    internal interface ILocalFileService
    {
        Stream ReadFile(string path);
    }
}
