using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNet.BoardGameMall.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "이메일 주소")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "코드")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "이 브라우저 기억")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "이메일 주소")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "이메일 주소")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "비밀번호")]
        public string Password { get; set; }

        [Display(Name = "사용자 계정 및 암호 저장")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "이메일 주소")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0}은(는) {2}자 이상이어야 합니다.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "비밀번호")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "비밀번호 확인")]
        [Compare("Password", ErrorMessage = "비밀번호와 확인 비밀번호가 일치하지 않습니다.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "이메일 주소")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0}은(는) {2}자 이상이어야 합니다.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "비밀번호")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "비밀번호 확인")]
        [Compare("Password", ErrorMessage = "비밀번호 확인 비밀번호가 일치하지 않습니다.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "이메일 주소")]
        public string Email { get; set; }
    }
}
