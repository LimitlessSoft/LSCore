using System.ComponentModel;
using System.Reflection;

namespace LSCore.Common.Extensions;

public static class EnumExtensions
{
	/// <summary>
	/// Returns the description of the enum contained in the DescriptionAttribute or throws an exception if not found
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public static string GetDescription(this Enum value) =>
		value.GetDescriptionOrDefault(DefaultValueType.Null)
		?? throw new NullReferenceException("Description attribute not found");

	/// <summary>
	/// Returns the description of the enum contained in the DescriptionAttribute or default value
	/// </summary>
	/// <param name="value"></param>
	/// <param name="defaultValueType"></param>
	/// <returns></returns>
	public static string? GetDescriptionOrDefault(
		this Enum value,
		DefaultValueType defaultValueType
	)
	{
		var field = value.GetType().GetField(value.ToString());
		var attribute = field?.GetCustomAttribute<DescriptionAttribute>();

		return attribute switch
		{
			null when defaultValueType == DefaultValueType.Self => value.ToString(),
			null when defaultValueType == DefaultValueType.Empty => string.Empty,
			_ => attribute?.Description
		};
	}
}
