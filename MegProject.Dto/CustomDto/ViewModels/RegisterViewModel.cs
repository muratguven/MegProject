using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MegProject.Dto.CustomDto.ComponentModels;

namespace MegProject.Dto.CustomDto.ViewModels
{
    [DataContract]
    [Serializable]
    public class RegisterViewModel
    {
        [DataMember]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DataMember]
        [ScaffoldColumn(false)]
        public int UserGroupId { get; set; }

        [DataMember]
        [DataType(DataType.EmailAddress,ErrorMessage = "Lütfen geçerli bir email adresi giriniz")]
        [Required(ErrorMessage = "Boş Bırakılamaz!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataMember]
        [StringLength(32,ErrorMessage = "Maksimum 32 karakter boyutunda olmalıdır!")]
        [Required(ErrorMessage = "Boş Bırakılamaz!")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [DataMember]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Boş Bırakılamaz!")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [DataMember]
        [DataType(DataType.Password)]
        [Display(Name = "Tekrar Şifre")]
        [Compare("Password", ErrorMessage = "Şifre alanları eşleşmiyor!")]
        public string ConfirmPassword { get; set; }

        
        [DataMember]
        [Required(ErrorMessage = "Lütfen Kullanıcının Rolünü Seçiniz")]
        [Display(Name = "Kullanıcı Rolü")]
        public List<CheckBoxModel> Roles { get; set; }  
    }
}