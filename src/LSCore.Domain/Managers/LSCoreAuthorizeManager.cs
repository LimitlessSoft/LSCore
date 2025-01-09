using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LSCore.Contracts.Configurations;
using LSCore.Contracts.Dtos;
using LSCore.Contracts.Exceptions;
using LSCore.Contracts.IManagers;
using LSCore.Contracts.Interfaces.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace LSCore.Domain.Managers;

public class LSCoreAuthorizeManager(
    LSCoreAuthorizationConfiguration authorizationConfiguration,
    ILSCoreAuthorizableEntityRepository authorizableEntityRepository)
    : ILSCoreAuthorizeManager
{
    public virtual LSCoreJwtDto Authorize<T>(T identifier, string password)
    {
        var authorizableEntity = authorizableEntityRepository.Get(identifier);
        if (authorizableEntity == null)
            throw new LSCoreNotFoundException();

        if (!BCrypt.Net.BCrypt.EnhancedVerify(password, authorizableEntity.Password))
            throw new LSCoreForbiddenException();

        var accessToken = GenerateJwt(identifier!.ToString(), TimeSpan.FromMinutes(30));
        var refreshToken = GenerateJwt(identifier, TimeSpan.FromDays(7));
     
        authorizableEntityRepository.SetRefreshToken(identifier, refreshToken);
        
        return new LSCoreJwtDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    private string GenerateJwt<T>(T identifier, TimeSpan expirationInterval)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authorizationConfiguration.SecurityKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new []
        {
            new Claim(JwtRegisteredClaimNames.Sub, identifier!.ToString()!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            authorizationConfiguration.Issuer,
            authorizationConfiguration.Audience,
            claims,
            expires: DateTime.Now.Add(expirationInterval),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}