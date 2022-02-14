using System.ComponentModel.DataAnnotations;

namespace ItServiceApp.Core.ViewModels
{
    public class LoginViewModel
    {
        

        [Display(Name = "Kullanıcı Ad")]
        [Required(ErrorMessage ="Kullanıcı adı gereklidir")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Şifre alanı gereklidir.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifreniz minimum 6 karakterli olmalıdır!")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Şifre")]
        public bool RememberMe { get; set; }


    }
}
