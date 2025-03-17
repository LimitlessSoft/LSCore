using FluentValidation;
using LSCore.Validation.Domain;
using Sample.ValidationWithRepository.Api.Interfaces;
using Sample.ValidationWithRepository.Api.Requests;

namespace Sample.ValidationWithRepository.Api.Validators;

public class UserRegisterRequestValidator : LSCoreValidatorBase<UserRegisterRequest>
{
	public UserRegisterRequestValidator(IUserRepository userRepository)
	{
		// You will never catch IUserRepository since usually it is scoped and validators are singleton/transient
		// Instead, force yourself to use factory for your dbcontext, and then extract things here
		RuleFor(x => x.Username)
			.Must(username => userRepository.UsernameOccupied(username) == false)
			.WithMessage("User with given username already exists");
	}
}
