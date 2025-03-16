using LSCore.Validation.Contracts;

namespace Sample.Validation.Api.Enums.ValidationCodes;

public enum UsersValidationCodes
{
	[LSCoreValidationMessage("Password must contain only letters and numbers.")]
	UVC_001,

	[LSCoreValidationMessage("Password must contains at least one number and one letter.")]
	UVC_002
}
