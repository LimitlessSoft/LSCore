using System.Text;
using LSCore.Auth.Contracts;
using LSCore.Auth.UserPass.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace LSCore.Auth.UserPass.DependencyInjection;

public static class Extensions
{
	public static void AddLSCoreAuthUserPass<
		TEntityIdentifier,
		TAuthPasswordManager,
		TLSCoreIdentityRepository
	>(this WebApplicationBuilder builder, LSCoreAuthUserPassConfiguration configuration)
		where TAuthPasswordManager : class, ILSCoreAuthPasswordManager<TEntityIdentifier>
		where TLSCoreIdentityRepository : class,
			ILSCoreAuthUserPassIdentityEntityRepository<TEntityIdentifier>
	{
		builder.Services.AddScoped<LSCoreAuthContextEntity<TEntityIdentifier>>();
		builder.Services.AddSingleton(configuration);
		builder
			.Services.AddAuthentication()
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidIssuer = configuration.Issuer,
					ValidAudience = configuration.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(configuration.SecurityKey)
					),
					RequireExpirationTime = true,
					ClockSkew = configuration.TokenSkew,
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true
				};
			});
		builder.Services.AddAuthorization();
		builder.Services.AddScoped<
			ILSCoreAuthPasswordManager<TEntityIdentifier>,
			TAuthPasswordManager
		>();
		builder.Services.AddScoped<
			ILSCoreAuthUserPassIdentityEntityRepository<TEntityIdentifier>,
			TLSCoreIdentityRepository
		>();
	}

	public static void UseLSCoreAuthUserPass<TEntityIdentifier>(this WebApplication app)
	{
		app.UseAuthentication();
		app.UseMiddleware<LSCoreAuthUserPassMiddleware<TEntityIdentifier>>();
	}
}
