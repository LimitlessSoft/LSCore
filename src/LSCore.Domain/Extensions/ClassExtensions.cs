using LSCore.Contracts.Exceptions;
using LSCore.Domain.Validators;
using Newtonsoft.Json;

namespace LSCore.Domain.Extensions;

public static class ClassExtensions
{
    public static void Validate<T>(this T sender)
        where T : class
    {
        // Get LSCoreValidatorBase<T> instance
        var validator = LSCoreDomainConstants.Container!.TryGetInstance<LSCoreValidatorBase<T>>();
        if (validator == null)
            throw new NullReferenceException(nameof(validator));

        var validationResult = validator.Validate(sender);
        if (!validationResult.IsValid)
            throw new LSCoreBadRequestException(JsonConvert.SerializeObject(validationResult.Errors));
    }
}