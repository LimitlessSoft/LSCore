using System.Reflection;

namespace LSCore.DependencyInjection;

public class LSCoreDependencyInjectionScanningOptions
{
	/// <summary>
	/// By default, the scanner will use default conventions to scan the assemblies.
	/// Default conventions mean that the scanner will scan all classes that have interfaces with the same name prefixed with <c>I</c>.
	/// Disable this if you want to use your own conventions.
	/// Default: true
	/// </summary>
	public void DisableDefaultConventions() => Constants.WithDefaultConventions = false;

	/// <summary>
	/// By default, the scanner will include the calling assembly as part of the scanning process.
	/// Disable this if you want to exclude the calling assembly.
	/// </summary>
	public void DisableCallingAssembly() => Constants.IncludeCallingAssembly = false;

	/// <summary>
	/// By default, the scanner will include classes inheriting ILSCoreDtoMappers as part of the scanning process.
	/// Mappers are in form of <c>YourDtoClassMapper : ILSCoreDtoMapper&lt;SourceClass, YourDto&gt;</c>.
	/// Mappers are used to map the source class to the dto class with simple <c>source.ToDto&lt;SourceClass, YourDto&gt;()</c> method.
	/// </summary>
	public void DisableLSCoreDtoMappers() => Constants.IncludeLSCoreMappers = false;

	/// <summary>
	/// By default, the scanner will include classes inheriting LSCoreValidatorBase as part of the scanning process.
	/// Validators are used to validate object properties against the rules defined in the validator class.
	/// Validators are in form of <c>YourClassValidator : LSCoreValidatorBase&lt;YourClass&gt;</c>.
	/// To validate an object, simply call <c>object.Validate()</c> extension from LSCore.Domain.
	/// If validation fails, LSCoreBadRequestException will be thrown.
	/// You can auto process these exceptions by using LSCoreHandleExceptionMiddleware.
	/// </summary>
	public void DisableLSCoreValidators() => Constants.IncludeLSCoreValidators = false;

	/// <summary>
	/// Define a predicate to filter the assemblies and executables that will be scanned.
	/// Rules defined here are applied after the default rule which is to scan all assemblies starting with the project root name.
	/// </summary>
	/// <param name="predicate"></param>
	public void SetShouldScanAssemblyPredicate(Func<Assembly, bool> predicate) =>
		Constants.ShouldScanAssemblyPredicate = predicate;
}
