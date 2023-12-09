using JasperFx.CodeGeneration.Frames;
using LSCore.Contracts;
using LSCore.Contracts.Extensions;
using LSCore.Contracts.Http;
using LSCore.Domain.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SP.Simple.Contracts.Enums.ValidationCodes;
using SP.Simple.Contracts.IManagers;
using SP.Simple.Contracts.Requests.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SP.Simple.Domain.Managers
{
    public class UserManager : LSCoreBaseManager<UserManager>, IUserManager
    {
        private readonly IConfigurationRoot _configurationRoot;
        public UserManager(ILogger<UserManager> logger, IConfigurationRoot configurationRoot)
            : base(logger)
        {
            _configurationRoot = configurationRoot;
        }

        public LSCoreResponse<string> Login(LoginUserRequest request)
        {
            if (request.Username == "TestUser" && request.Password == "TestPassword")
                return new LSCoreResponse<string>(GenerateJSONWebToken("TestUser"));

            return LSCoreResponse<string>.BadRequest(UsersValidationCodes.UVC_001.GetDescription()!);
        }

        public LSCoreResponse<string> Me() =>
            new LSCoreResponse<string>(CurrentUser.Username);

        private string GenerateJSONWebToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationRoot["JWT_KEY"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(LSCoreContractsConstants.ClaimNames.CustomUsername, username),
                new Claim(LSCoreContractsConstants.ClaimNames.CustomUserId, 123.ToString()), // 123 is some custom id from your local database
            };

            var jwtIssuer = _configurationRoot["JWT_ISSUER"];
            var jwtAudience = _configurationRoot["JWT_AUDIENCE"];
            var token = new JwtSecurityToken(jwtIssuer, jwtAudience,
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
