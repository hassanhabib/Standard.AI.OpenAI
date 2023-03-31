// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.IO;

namespace Standard.AI.OpenAI.Tests.Unit
{
    internal sealed class AutoRemovableFile : IDisposable
    {
        public readonly string FilePath;

        public AutoRemovableFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new System.ArgumentException($"'{nameof(filePath)}' cannot be null or whitespace.", nameof(filePath));

            this.FilePath = filePath;
            EnsureFileCreated();
        }

        public void Dispose()
        {
            EnsureFileDeleted();
        }

        private void EnsureFileCreated()
        {
            if (Path.Exists(this.FilePath))
                return;

            File.WriteAllText(this.FilePath, "");
        }

        private void EnsureFileDeleted()
        {
            if (!Path.Exists(this.FilePath))
                return;

            File.Delete(this.FilePath);
        }
    }
}
