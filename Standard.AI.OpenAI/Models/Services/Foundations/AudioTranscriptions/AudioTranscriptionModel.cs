// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;

namespace Standard.AI.OpenAI.Models.Services.Foundations.AudioTranscriptions
{
#nullable enable
    public readonly struct AudioTranscriptionModel : IEquatable<AudioTranscriptionModel>
    {
        public static AudioTranscriptionModel Whisper1 { get; } = new AudioTranscriptionModel("whisper-1");

        internal string Name { get; }

        private AudioTranscriptionModel(string osPlatform)
        {
            ArgumentException.ThrowIfNullOrEmpty(osPlatform);
            Name = osPlatform;
        }

        /// <summary>
        /// Creates a new <see cref="AudioTranscriptionModel"/> instance.
        /// </summary>
        /// <remarks>If you plan to call this method frequently, please consider caching its result.</remarks>
        public static AudioTranscriptionModel Create(string osPlatform)
        {
            return new AudioTranscriptionModel(osPlatform);
        }

        public bool Equals(AudioTranscriptionModel other)
        {
            return Equals(other.Name);
        }

        internal bool Equals(string? other)
        {
            return string.Equals(Name, other, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is AudioTranscriptionModel osPlatform && Equals(osPlatform);
        }

        public override int GetHashCode()
        {
            return Name == null ? 0 : StringComparer.OrdinalIgnoreCase.GetHashCode(Name);
        }

        public override string ToString()
        {
            return Name ?? string.Empty;
        }

        public static bool operator ==(AudioTranscriptionModel left, AudioTranscriptionModel right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AudioTranscriptionModel left, AudioTranscriptionModel right)
        {
            return !(left == right);
        }

        public static implicit operator string(AudioTranscriptionModel other)
        {
            return other.Name;
        }
    }
}
