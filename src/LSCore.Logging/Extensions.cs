using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace LSCore.Logging;

public static class Extensions
{
	/// <summary>
	/// Configures the logging system to include console and debug providers.
	/// </summary>
	/// <param name="builder">The web application builder.</param>
	/// <returns>The logging builder.</returns>
	public static ILoggingBuilder AddLSCoreLogging(this WebApplicationBuilder builder)
	{
		builder.Logging.AddConsole();
		builder.Logging.AddDebug();
		return builder.Logging;
	}
}
