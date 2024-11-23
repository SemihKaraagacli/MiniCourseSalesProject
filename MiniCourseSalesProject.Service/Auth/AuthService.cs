using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiniCourseSalesProject.Repository.Entities;
using MiniCourseSalesProject.Repository.OrderRepository;
using MiniCourseSalesProject.Service.Auth.Dtos;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace MiniCourseSalesProject.Service.Auth
{
    public class AuthService(UserManager<AppUser> userManager, IOrderRepository orderRepository, IConfiguration configuration) : IAuthService
    {
        public async Task<ServiceResult<TokenResponse>> SignInAsync(SignInRequest signInRequest)
        {
            var hasUser = await userManager.FindByEmailAsync(signInRequest.Email);
            if (hasUser is null)
            {
                return ServiceResult<TokenResponse>.Fail("Email or password wrong", HttpStatusCode.BadRequest);
            }
            var result = await userManager.CheckPasswordAsync(hasUser, signInRequest.Password);
            if (!result)
            {
                return ServiceResult<TokenResponse>.Fail("Email or password wrong", HttpStatusCode.BadRequest);
            }

            var userClaims = new List<Claim>();

            userClaims.Add(new Claim(ClaimTypes.NameIdentifier, hasUser.Id.ToString()));
            userClaims.Add(new Claim(ClaimTypes.Name, hasUser.UserName));
            userClaims.Add(new Claim(ClaimTypes.Email, hasUser.Email));
            userClaims.Add(new Claim("token_id", Guid.NewGuid().ToString()));
            userClaims.Add(new Claim("Wallet", hasUser.Wallet.ToString()));



            var roles = await userManager.GetRolesAsync(hasUser);
            foreach (var role in roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            JwtSecurityToken newToken = new JwtSecurityToken(
                issuer: configuration["TokenOptions:Issuer"],
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenOptions:SymmetricKey"]!)), SecurityAlgorithms.HmacSha256)
                );
            var accessTokenAsString = new JwtSecurityTokenHandler().WriteToken(newToken);

            return ServiceResult<TokenResponse>.Success(new TokenResponse(accessTokenAsString), HttpStatusCode.OK);
        }
        public Task<ServiceResult<TokenResponse>> SignInClientCredentialAsync(SignInClientCredentialRequest request)
        {
            var clientId = configuration.GetSection("Clients")["ClientId"];
            var clientSecret = configuration.GetSection("Clients")["ClientSecret"];

            if (request.ClientId != clientId || request!.ClientSecret != clientSecret)
            {
                return Task.FromResult(ServiceResult<TokenResponse>.Fail("clientId or clientsecret is wrong",
                    HttpStatusCode.BadRequest));
            }

            var clientClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, clientId),
                new Claim("token_id", Guid.NewGuid().ToString())
            };


            JwtSecurityToken newToken = new JwtSecurityToken(
                issuer: configuration["TokenOptions:Issuer"],
                claims: clientClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenOptions:SymmetricKey"]!)), SecurityAlgorithms.HmacSha256)
                );
            var accessTokenAsString = new JwtSecurityTokenHandler().WriteToken(newToken);

            return Task.FromResult(ServiceResult<TokenResponse>.Success(new TokenResponse(accessTokenAsString), HttpStatusCode.OK));
        }
    }
}
