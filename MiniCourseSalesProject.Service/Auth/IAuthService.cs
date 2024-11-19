using MiniCourseSalesProject.Service.Auth.Dtos;

namespace MiniCourseSalesProject.Service.Auth
{
    public interface IAuthService
    {
        Task<ServiceResult<TokenResponse>> SignInAsync(SignInRequest signInRequest);
        Task<ServiceResult<TokenResponse>> SignInClientCredentialAsync(SignInClientCredentialRequest request);
    }
}