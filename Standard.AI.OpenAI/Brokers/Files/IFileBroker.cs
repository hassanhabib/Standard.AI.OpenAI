// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.IO;

namespace Standard.AI.OpenAI.Brokers.Files
{
    internal interface IFileBroker
    {
        Stream ReadFile(string path);
    }
}
