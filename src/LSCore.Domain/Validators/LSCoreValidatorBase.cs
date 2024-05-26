using FluentValidation;
using LSCore.Contracts.IValidators;

namespace LSCore.Domain.Validators;

public class LSCoreValidatorBase<TRequest> : AbstractValidator<TRequest>, ILSCoreValidator<TRequest>;
