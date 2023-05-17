using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestaurantBooking.Application.Services.Authentication.Models;
using RestaurantBooking.Application.Services.RoleService;
using RestaurantBooking.Application.Services.UserService;
using RestaurantBooking.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RestaurantBooking.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtOptions jwtOption;
        private readonly IUserService userService;
        private readonly IRoleService roleService;

        public AuthenticationService(IUserService userService, IRoleService roleService, IOptions<JwtOptions> jwtOption)
        {
            this.jwtOption = jwtOption.Value;
            this.userService = userService;
            this.roleService = roleService;
        }

        public User RegisterUser(RegisterModel registerModel, bool isAdmin = false)
        {
            if (userService.IsExist(registerModel.Email))
            {
                throw new Exception("Email is already taken");
            }

            User userModel = registerModel.ToUser();

            userModel.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userModel.PasswordHash);

            if (isAdmin)
                userModel.Roles.Add(roleService.GetRoleByName("Admin"));
            else
                userModel.Roles.Add(roleService.GetRoleByName("Member"));

            userService.Add(userModel);

            return userModel;
        }

        public AuthenticatedModel AuthUser(LoginModel loginModel)
        {
            var user = userService.GetByEmail(loginModel.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginModel.Password, user.PasswordHash))
            {
                throw new Exception("Invalid Email or password");
            }

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in user.Roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            (var token, string refreshToken) = GenerateTokens(authClaims);

            userService.SetRefreshToken(user.Id, refreshToken, DateTime.Now.AddDays(jwtOption.RefreshTokenValidityInDays));

            return new AuthenticatedModel(new JwtSecurityTokenHandler().WriteToken(token), refreshToken, token.ValidTo);
        }

        public AuthenticatedModel RefreshToken(TokenRefreshModel tokenRefreshModel)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Secret)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(tokenRefreshModel.AccessToken, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            string email = principal.Identity!.Name!;

            var user = userService.GetByEmail(email);

            if (user == null || user.RefreshToken != tokenRefreshModel.RefreshToken || user.RefreshTokenExpiring <= DateTime.Now)
                throw new SecurityTokenException("Refresh token are not valid");

            (var token, string refreshToken) = GenerateTokens(principal.Claims);

            userService.SetRefreshToken(user.Id, refreshToken, DateTime.Now.AddDays(jwtOption.RefreshTokenValidityInDays));

            return new AuthenticatedModel(new JwtSecurityTokenHandler().WriteToken(token), refreshToken, token.ValidTo);
        }

        private (JwtSecurityToken accessToken, string refreshToken) GenerateTokens(IEnumerable<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Secret));

            var token = new JwtSecurityToken(
                issuer: jwtOption.ValidIssuer,
                audience: jwtOption.ValidAudience,
                expires: DateTime.Now.AddMinutes(jwtOption.TokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var refreshToken = Convert.ToBase64String(randomNumber);

            return (token, refreshToken);
        }
    }
}
