using System.ComponentModel.DataAnnotations;

namespace MegProject.Data.Models
{
    public class SystemUsers
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email boş olamaz")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir email adresi giriniz!")]
        public string Email { get; set; }

        [Required]
        [MinLength(6,ErrorMessage = "Şifreniz minimum altı karakter olmalıdır!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Boş bırakılamaz!")]
        [Compare("Password",ErrorMessage = "Girdiğiniz şifre uyuşmuyor!")]
        public string ConfirmPassword { get; set; }


    }
}