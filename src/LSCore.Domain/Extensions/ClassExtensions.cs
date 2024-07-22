using LSCore.Contracts.Exceptions;
using LSCore.DependencyInjection;
using LSCore.Domain.Validators;
using Newtonsoft.Json;

namespace LSCore.Domain.Extensions;

public static class ClassExtensions
{
    public static void Validate<T>(this T sender)
        where T : class
    {
        if (Container.ServiceProvider == null)
            throw new Exception("To be able to use Validate method, you must call IHost.UseLSCoreDependencyInjection() as first method after you build application.");
        
        var validator = (LSCoreValidatorBase<T>?)Container.ServiceProvider.GetService(typeof(LSCoreValidatorBase<T>));
        if (validator == null)
            throw new NullReferenceException(nameof(validator));
        
        var validationResult = validator.Validate(sender);
        if (!validationResult.IsValid)
            throw new LSCoreBadRequestException(JsonConvert.SerializeObject(validationResult.Errors));
    }
}