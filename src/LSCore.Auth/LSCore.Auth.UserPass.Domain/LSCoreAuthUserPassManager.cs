using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using LSCore.Auth.Contracts;
using LSCore.Auth.Contracts.Constants;
using LSCore.Auth.UserPass.Contracts;
using LSCore.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace LSCore.Auth.UserPass.Domain;

public class LSCoreAuthUserPassManager<TEntityIdentifier>(
	ILSCoreAuthUserPassIdentityEntityRepository<TEntityIdentifier> userPassIdentityEntityRepository,
	LSCoreAuthUserPassConfiguration configuration
) : ILSCoreAuthUserPassManager<TEntityIdentifier>
{
	public LSCoreJwt Authenticate(TEntityIdentifier identifier, string password)
	{
		var entity = userPassIdentityEntityRepository.GetOrDefault(identifier);
		if (entity == null)
			throw new LSCoreForbiddenException();

		try
		{
			if (!BCrypt.Net.BCrypt.EnhancedVerify(password, entity.Password))
				throw new LSCoreForbiddenException();
		}
		catch (SaltParseException)
		{
			// Expected if password is not saved as a BCrypt hash
			throw new LSCoreForbiddenException();
		}

		var accessToken = GenerateJwt(
			entity.Identifier,
			TimeSpan.FromMinutes(configuration.AccessTokenExpirationMinutes)
		);
		var refreshToken = GenerateJwt(
			entity.Identifier,
			TimeSpan.FromDays(configuration.RefreshTokenExpirationDays)
		);

		userPassIdentityEntityRepository.SetRefreshToken(entity.Identifier, refreshToken);

		return new LSCoreJwt { AccessToken = accessToken, RefreshToken = refreshToken };
	}

	/// <summary>
	/// Generates a JWT token using the provided <paramref name="identifier"/> and
	/// <paramref name="expirationInterval"/>.
	/// </summary>
	/// <param name="identifier">The unique identifier of the user.</param>
	/// <param name="expirationInterval">The interval after which the token will expire.</param>
	/// <returns>Returns a string representing the generated JWT token.</returns>
	private string GenerateJwt(TEntityIdentifier identifier, TimeSpan expirationInterval)
	{
		var securityKey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(configuration.SecurityKey)
		);
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, identifier!.ToString()!),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new Claim(LSCoreAuthClaims.Identifier, identifier!.ToString()!)
		};

		var token = new JwtSecurityToken(
			configuration.Issuer,
			configuration.Audience,
			claims,
			expires: DateTime.Now.Add(expirationInterval),
			signingCredentials: credentials
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
