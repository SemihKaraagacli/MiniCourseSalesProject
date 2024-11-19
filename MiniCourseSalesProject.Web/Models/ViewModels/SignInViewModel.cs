using System.ComponentModel.DataAnnotations;

namespace MiniCourseSalesProject.Web.Models.ViewModels
{
    public class SignInViewModel
    {
        [EmailAddress] public string Email { get; set; } = default!;
        [DataType(DataType.Password)] public string Password { get; set; } = default!;
        public bool RememberMe { get; set; }
    }
}
