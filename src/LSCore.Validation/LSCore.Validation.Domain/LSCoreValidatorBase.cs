using FluentValidation;
using LSCore.Validation.Contracts;

namespace LSCore.Validation.Domain;

public class LSCoreValidatorBase<TRequest> : AbstractValidator<TRequest>, ILSCoreValidator<TRequest>
	where TRequest : class;
