using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MegProject.Dto.CustomDto.ViewModels
{
    [DataContract]
    [Serializable]
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Boş bırakılamaz!")]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Boş bırakılamaz!")]
        [DataType(DataType.Password)]
        public  string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}