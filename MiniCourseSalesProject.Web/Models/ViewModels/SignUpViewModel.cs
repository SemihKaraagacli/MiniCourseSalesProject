using System.ComponentModel.DataAnnotations;

namespace MiniCourseSalesProject.Web.Models.ViewModels
{
    public class SignUpViewModel
    {
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public decimal Wallet { get; set; }
    }
}
