using System.Text.RegularExpressions;
using FluentValidation;
using LSCore.Validation.Contracts;
using LSCore.Validation.Domain;
using Sample.Validation.Api.Enums.ValidationCodes;
using Sample.Validation.Api.Requests;

namespace Sample.Validation.Api.Validators;

public class UserRegisterRequestValidator : LSCoreValidatorBase<UserRegisterRequest>
{
	const int MIN_PASSWORD_LENGTH = 8;
	const string PASSWORD_REGEX_ONLY_LETTERS_AND_NUMBERS = @"^[A-Za-z\d]+$";
	private const string PASSWORD_REGEX_AT_LEAST_ONE_LETTER_AND_ONE_NUMBER =
		@"^(?=.*[A-Za-z])(?=.*\d).+$";
	const int MIN_USERNAME_LENGTH = 5;
	const int MAX_UERNAME_LENGTH = 20;

	public UserRegisterRequestValidator()
	{
		RuleFor(x => x.Username)
			.NotEmpty()
			.MinimumLength(MIN_USERNAME_LENGTH)
			.MaximumLength(MAX_UERNAME_LENGTH);

		RuleFor(x => x.Password)
			.NotEmpty()
			.MinimumLength(MIN_PASSWORD_LENGTH)
			.Must(x => Regex.IsMatch(x, PASSWORD_REGEX_ONLY_LETTERS_AND_NUMBERS))
			.WithMessage(UsersValidationCodes.UVC_001.GetValidationMessage())
			.Must(x => Regex.IsMatch(x, PASSWORD_REGEX_AT_LEAST_ONE_LETTER_AND_ONE_NUMBER))
			.WithMessage(UsersValidationCodes.UVC_002.GetValidationMessage());
	}
}
