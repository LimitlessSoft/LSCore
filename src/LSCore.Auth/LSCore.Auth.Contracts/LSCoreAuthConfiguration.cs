using Microsoft.AspNetCore.Authentication;

namespace LSCore.Auth.Contracts;

public abstract class LSCoreAuthConfiguration : AuthenticationSchemeOptions
{
	public bool AuthAll { get; set; }
	public bool BreakOnFailedAuth { get; set; } = true;
}
