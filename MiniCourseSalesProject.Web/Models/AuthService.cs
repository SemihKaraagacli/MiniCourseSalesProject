using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MiniCourseSalesProject.Web.Models.Dtos;
using MiniCourseSalesProject.Web.Models.ViewModels;
using System.Security.Claims;

namespace MiniCourseSalesProject.Web.Models
{
    public class AuthService(HttpClient client, IHttpContextAccessor httpContextAccessor)
    {
        public async Task<ServiceResult> SignInAsync(SignInViewModel viewModel)
        {
            var adress = "/Auth/signin";

            var response = await client.PostAsJsonAsync<SignInViewModel>(adress, viewModel);  //Fecth

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                return ServiceResult.Fail(problemDetails!.Detail!);
            }
            var tokenResponse = await response.Content.ReadFromJsonAsync<SignInResponse>(); // gelen parse edilmemiş AccessToken

            JsonWebTokenHandler tokenHandler = new JsonWebTokenHandler(); //Parse
            var jwtDecoded = tokenHandler.ReadJsonWebToken(tokenResponse.AccessToken); //Parse edilmiş token

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(jwtDecoded.Claims, CookieAuthenticationDefaults.AuthenticationScheme); //decode edilen token içindeki claimlerden belirtilen cookie semasına göre bir ıdentity oluşturuyor .

            ClaimsPrincipal cliamPrincipal = new ClaimsPrincipal(claimsIdentity); //Verilen claimsleri tut

            AuthenticationProperties authenticationProperties = new AuthenticationProperties //kullanıcı oturumunun süresini veya kalıcılığını kontrol et.
            {
                IsPersistent = viewModel.RememberMe,
            };
            var accessToken = new AuthenticationToken() //Clientde görünecek Token Model
            {
                Name = OpenIdConnectParameterNames.AccessToken,
                Value = tokenResponse.AccessToken
            };

            authenticationProperties.StoreTokens([accessToken]); //kullanıcıya ait tokenı oturum bilgilerine ekle

            await httpContextAccessor.HttpContext!.SignInAsync(cliamPrincipal, authenticationProperties);// simetrik şifreleme ile cookie oluştur, verilen token modeli cookieye ekle


            return ServiceResult.Success();
        }
    }
}
