using System.Net;
using LSCore.Auth.Key.Contracts;
using LSCore.Exceptions;

namespace LSCore.ApiClient.Rest;

public abstract class LSCoreApiClient
{
	protected readonly HttpClient _httpClient = new();

	public LSCoreApiClient(ILSCoreApiClientRestConfiguration configuration)
	{
		_httpClient.BaseAddress = new Uri(configuration.BaseUrl);
		if (!string.IsNullOrWhiteSpace(configuration.LSCoreApiKey))
			_httpClient.DefaultRequestHeaders.Add(
				LSCoreAuthKeyHeaders.KeyCustomHeader,
				configuration.LSCoreApiKey
			);
	}

	protected void HandleStatusCode(HttpResponseMessage? response)
	{
		if (response == null)
			throw new LSCoreBadRequestException("Response is null.");

		switch (response.StatusCode)
		{
			case HttpStatusCode.OK:
				return;
			case HttpStatusCode.BadRequest:
				throw new LSCoreBadRequestException("Microservice API returned bad request.");
			case HttpStatusCode.Unauthorized:
				throw new LSCoreUnauthenticatedException();
			case HttpStatusCode.Forbidden:
				throw new LSCoreForbiddenException();
			case HttpStatusCode.NotFound:
				throw new LSCoreNotFoundException();
			default:
				throw new LSCoreBadRequestException(
					"Microservice API returned unhandled exception."
				);
		}
	}
}
