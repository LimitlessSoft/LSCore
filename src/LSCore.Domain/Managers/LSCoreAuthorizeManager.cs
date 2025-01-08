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
    public virtual LSCoreJwtDto Authorize(string username, string password)
    {
        var authorizableEntity = authorizableEntityRepository.Get(username);
        if (authorizableEntity == null)
            throw new LSCoreNotFoundException();

        if (!BCrypt.Net.BCrypt.EnhancedVerify(password, authorizableEntity.Password))
            throw new LSCoreForbiddenException();

        var accessToken = GenerateJwt(username, TimeSpan.FromMinutes(30));
        var refreshToken = GenerateJwt(username, TimeSpan.FromDays(7));
     
        authorizableEntityRepository.SetRefreshToken(username, refreshToken);
        
        return new LSCoreJwtDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    private string GenerateJwt(string username, TimeSpan expirationInterval)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authorizationConfiguration.SecurityKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256);
        var claims = new []
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
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