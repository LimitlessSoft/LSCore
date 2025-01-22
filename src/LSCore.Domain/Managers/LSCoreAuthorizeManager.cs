using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using LSCore.Contracts;
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
            throw new LSCoreForbiddenException();

        try
        {
            if (!BCrypt.Net.BCrypt.EnhancedVerify(password, authorizableEntity.Password))
                throw new LSCoreForbiddenException();
        }
        catch (SaltParseException e)
        {
            // Expected if password is not saved as a BCrypt hash
            throw new LSCoreForbiddenException();
        }

        var accessToken = GenerateJwt(authorizableEntity.Id, TimeSpan.FromMinutes(authorizationConfiguration.AccessTokenExpirationMinutes));
        var refreshToken = GenerateJwt(authorizableEntity.Id, TimeSpan.FromDays(authorizationConfiguration.RefreshTokenExpirationDays));
     
        authorizableEntityRepository.SetRefreshToken(authorizableEntity.Id, refreshToken);
        
        return new LSCoreJwtDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
    
    public virtual LSCoreJwtDto Refresh(string refreshToken)
    {
        if(string.IsNullOrWhiteSpace(refreshToken))
            throw new LSCoreForbiddenException();
        
        var authorizableEntity = authorizableEntityRepository.GetByRefreshToken(refreshToken);
        if (authorizableEntity == null)
            throw new LSCoreForbiddenException();

        var accessToken = GenerateJwt(authorizableEntity.Id, TimeSpan.FromMinutes(authorizationConfiguration.AccessTokenExpirationMinutes));
        var newRefreshToken = GenerateJwt(authorizableEntity.Id, TimeSpan.FromDays(authorizationConfiguration.RefreshTokenExpirationDays));
        
        authorizableEntityRepository.SetRefreshToken(authorizableEntity.Id, newRefreshToken);
        
        return new LSCoreJwtDto
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken
        };
    }

    private string GenerateJwt(long id, TimeSpan expirationInterval)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authorizationConfiguration.SecurityKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new []
        {
            new Claim(JwtRegisteredClaimNames.Sub, id!.ToString()!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(LSCoreContractsConstants.ClaimNames.CustomIdentifier, id!.ToString()!)
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