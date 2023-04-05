// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.IO;

namespace Standard.AI.OpenAI.Brokers.Files
{
    internal class FileBroker : IFileBroker
    {
        public Stream ReadFile(string path) =>
            File.OpenRead(path);
    }
}
