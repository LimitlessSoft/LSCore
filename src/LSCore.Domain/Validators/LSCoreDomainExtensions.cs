using LSCore.Contracts.Http.Interfaces;
using FluentValidation;

namespace LSCore.Domain.Validators
{
    public static class LSCoreDomainExtensions
    {   
        public static bool IsRequestInvalid<TRequest, TResponse>(this TRequest request, TResponse response)
            where TResponse : ILSCoreResponse
        {
            var validator = (IValidator<TRequest>?)LSCoreDomainConstants.Container?.TryGetInstance(typeof(IValidator<TRequest>));
            if (validator == null)
                return false;

            var validationResult = validator.Validate(request);

            if(!validationResult.IsValid)
            {
                response.Status = System.Net.HttpStatusCode.BadRequest;
                response.Errors = new List<string>(validationResult.Errors.Select(x => x.ErrorMessage));
            }

            return !validator.Validate(request).IsValid;
        }
    }
}
