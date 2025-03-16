using System.Reflection;

namespace LSCore.Validation.Contracts;

public static class Extensions
{
	public static string GetValidationMessage(this Enum value)
	{
		var type = value.GetType();
		var field = type.GetField(value.ToString());
		var attribute = field?.GetCustomAttribute<LSCoreValidationMessageAttribute>();
		return attribute?.Description ?? value.ToString();
	}

	public static string GetValidationMessage(this Enum value, params object?[] args) =>
		string.Format(value.GetValidationMessage(), args);
}
