using LSCore.Contracts.Extensions;
using FluentValidation.Results;

namespace LSCore.Domain.Extensions;

public static class LSCoreEnumExtensions
{
    public static ValidationFailure ToValidationFailure(this Enum sender) =>
        new ValidationFailure("Validation error", sender.GetDescription()!);
}