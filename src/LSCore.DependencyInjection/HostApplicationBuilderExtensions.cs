using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LSCore.DependencyInjection;

public static class HostApplicationBuilderExtensions
{
	/// <summary>
	/// Scans assemblies and executables from the application base directory and adds services following options.
	/// Check out LSCoreDependencyInjectionOptions for default options.
	/// </summary>
	/// <param name="builder"></param>
	/// <param name="projectRootName">Having DDD structure, pass only root of the project so all assemblies starting with that name will be scanned.
	/// Example is MyProject.Api & MyProject.Contracts - only MyProject should be passed as projectRootName.</param>
	/// <param name="options">Use to extend and manually scan for other assemblies</param>
	public static void AddLSCoreDependencyInjection(
		this IHostApplicationBuilder builder,
		string projectRootName,
		Action<LSCoreDependencyInjectionOptions>? options = null
	)
	{
		var opts = new LSCoreDependencyInjectionOptions();
		opts.Scan.SetShouldScanAssemblyPredicate(
			(x) => x.GetName().Name!.StartsWith(projectRootName)
		);

		options?.Invoke(opts);

		#region Apply options
		if (Constants.IncludeCallingAssembly)
			Constants.Configuration.AssembliesToBeScanned.Add(Assembly.GetCallingAssembly());

		if (Constants.ShouldScanAssemblyPredicate != null)
			Constants.Configuration.AssembliesToBeScanned.AddRange(
				Directory
					.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
					.Select(Assembly.LoadFrom)
					.Where(Constants.ShouldScanAssemblyPredicate)
			);
		#endregion

		builder.InitializeLSCoreDependencyInjection();
	}

	private static void InitializeLSCoreDependencyInjection(this IHostApplicationBuilder builder)
	{
		foreach (
			var type in Constants
				.Configuration.AssembliesToBeScanned.Distinct()
				.SelectMany(a => a.GetTypes())
		)
		{
			if (Constants.WithDefaultConventions)
				builder.AddServicesFollowingDefaultConventions(type);

			if (Constants.IncludeLSCoreMappers)
				builder.AddServicesFollowingLSCoreMapperConventions(type);

			if (Constants.IncludeLSCoreValidators)
				builder.AddServicesFollowingLSCoreValidatorConventions(type);
		}
	}

	private static void AddServicesFollowingDefaultConventions(
		this IHostApplicationBuilder builder,
		Type? type
	)
	{
		if (type is not { IsClass: true } || type.IsAbstract || type.IsGenericType)
			return;

		var @interface = type.GetInterfaces().FirstOrDefault(x => x.Name == $"I{type.Name}");
		if (@interface == null)
			return;

		builder.Services.AddTransient(@interface, type);
	}

	private static void AddServicesFollowingLSCoreMapperConventions(
		this IHostApplicationBuilder builder,
		Type? type
	)
	{
		if (type is not { IsClass: true } || type.IsAbstract || type.IsGenericType)
			return;

		var @interface = type.GetInterfaces().FirstOrDefault(x => x.Name.Contains("ILSCoreMapper"));
		if (@interface == null)
			return;

		builder.Services.AddSingleton(@interface, type);
	}

	private static void AddServicesFollowingLSCoreValidatorConventions(
		this IHostApplicationBuilder builder,
		Type? type
	)
	{
		if (type is not { IsClass: true } || type.IsAbstract || type.IsGenericType)
			return;

		var baseType = type.BaseType;
		while (baseType != null)
		{
			if (
				baseType.IsGenericType
				&& baseType.GetGenericTypeDefinition().Name.Contains("LSCoreValidatorBase")
			)
			{
				builder.Services.AddTransient(baseType, type);
				return;
			}
			baseType = baseType.BaseType;
		}
	}

	public static void UseLSCoreDependencyInjection(this IHost host)
	{
		Container.ServiceProvider = host.Services.GetService<IServiceProvider>();
	}
}
