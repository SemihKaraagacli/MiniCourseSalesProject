using FluentValidation;
using MiniCourseSalesProject.Web.Models.ViewModels;

namespace MiniCourseSalesProject.Web.Models.Validations
{
    public class SignUpValidator : AbstractValidator<SignUpViewModel>
    {
        public SignUpValidator()
        {
            RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
            .Length(3, 20).WithMessage("Kullanıcı adı 3 ile 20 karakter arasında olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir email adresi girin.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Parola boş olamaz.")
                .MinimumLength(6).WithMessage("Parola en az 6 karakter olmalıdır.");

            RuleFor(x => x.Wallet)
                .GreaterThan(0).WithMessage("Wallet 0'dan büyük olmalıdır.")
                .LessThanOrEqualTo(10000).WithMessage("Wallet değeri 10.000'i geçemez.");
        }
    }
}
