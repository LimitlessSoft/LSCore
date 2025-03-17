using FluentValidation;
using LSCore.Validation.Domain;
using Sample.ValidationWithRepository.Api.Interfaces;
using Sample.ValidationWithRepository.Api.Requests;

namespace Sample.ValidationWithRepository.Api.Validators;

public class UserRegisterRequestValidator : LSCoreValidatorBase<UserRegisterRequest>
{
	public UserRegisterRequestValidator(IUserRepository userRepository)
	{
		RuleFor(x => x.Username)
			.Must(username => userRepository.UsernameOccupied(username) == false)
			.WithMessage("User with given username already exists");
	}
}
