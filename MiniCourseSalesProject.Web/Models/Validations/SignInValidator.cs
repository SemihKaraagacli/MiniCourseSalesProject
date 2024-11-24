using FluentValidation;
using MiniCourseSalesProject.Web.Models.ViewModels;

namespace MiniCourseSalesProject.Web.Models.Validations
{
    public class SignInValidator : AbstractValidator<SignInViewModel>
    {
        public SignInValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Kullanıcı adı gereklidir.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre gereklidir.")
                .Length(6, 20).WithMessage("Şifre 6 ile 20 karakter arasında olmalıdır.");
        }
    }
}
