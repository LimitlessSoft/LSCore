using System.Reflection;

namespace LSCore.Contracts.Extensions;

public static class LSCoreEnumExtensions
{
    public static string? GetDescriptionOrDefault(this Enum value)
    {
        var fi = value.GetType().GetField(value.ToString(), BindingFlags.Public | BindingFlags.Static);

        return fi == null ? null : fi.GetDescription();
    }

    public static string GetDescription(this Enum value) =>
        value.GetDescriptionOrDefault()!;
}
