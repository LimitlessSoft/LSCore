using System.Reflection;

namespace LSCore.Contracts.Extensions
{
    public static class LSCoreEnumExtensions
    {
        public static string? GetDescription(this Enum value)
        {
            if (value == null)
                return null;

            var fi = value.GetType().GetField(value.ToString(), BindingFlags.Public | BindingFlags.Static);

            if (fi == null)
                return null;

            return fi.GetDescription();
        }
    }
}
