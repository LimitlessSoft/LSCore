using Microsoft.AspNetCore.Builder;

namespace LSCore.Exceptions.DependencyInjection;

public static class Extensions
{
	/// <summary>
	/// Used if you want to process LSCoreExceptions and alter response status code and message based on exception type.
	/// </summary>
	/// <param name="app"></param>
	public static void UseLSCoreExceptionsHandler(this WebApplication app)
	{
		app.UseMiddleware<LSCoreExceptionsHandleMiddleware>();
	}
}
