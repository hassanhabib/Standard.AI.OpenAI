// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AIFiles
{
    internal class FileResponse
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public int Size { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string FileName { get; set; }
        public string Purpose { get; set; }
    }
}
