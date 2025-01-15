using FluentValidation;
using LSCore.Domain.Validators;
using Validators.Contracts;

namespace Validators.Domain;

public class GetUsersRequestValidator : LSCoreValidatorBase<GetUsersRequest>
{
    public GetUsersRequestValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(10)
            .When(x => x.Name != null);
    }
}