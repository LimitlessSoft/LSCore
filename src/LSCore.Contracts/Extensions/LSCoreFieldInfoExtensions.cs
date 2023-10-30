using System.ComponentModel;
using System.Reflection;

namespace LSCore.Contracts.Extensions
{
    public static class LSCoreFieldInfoExtensions
    {
        public static string? GetDescription(this FieldInfo fieldInfo)
        {
            if (fieldInfo == null)
                return null;

            var attribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .OfType<DescriptionAttribute>().FirstOrDefault();

            return attribute == null ? fieldInfo.Name : attribute.Description;
        }
    }
}
