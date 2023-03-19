using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ConsoleApp1
{
    public static class EnumExtensions
    {
        public static bool TryGetDescription(this Enum val, [NotNullWhen(true)] out string? description)
        {
           var attributes = val
               .GetType()
               .GetField(val.ToString())!
               .GetCustomAttributes<DescriptionAttribute>()
               .ToArray();

           description = attributes.FirstOrDefault()?.Description;
           return !string.IsNullOrEmpty(description);
        }
    }
}