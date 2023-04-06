// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.IO;
using Standard.AI.OpenAI.Brokers.Files;

namespace Standard.AI.OpenAI.Services.Foundations.LocalFiles
{
    internal partial class LocalFileService : ILocalFileService
    {
        private readonly IFileBroker fileBroker;

        public LocalFileService(IFileBroker fileBroker) =>
            this.fileBroker = fileBroker;

        public Stream ReadFile(string path) =>
        TryCatch(() =>
        {
            ValidatePath(path);

            return this.fileBroker.ReadFile(path);
        });
    }
}
