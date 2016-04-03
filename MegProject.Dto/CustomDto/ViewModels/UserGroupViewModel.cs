using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MegProject.Dto.CustomDto.ViewModels
{
    [DataContract]
    [Serializable]
    public class UserGroupViewModel
    {
        [DataMember]
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Boş bırakılamaz!")]
        [Display(Name = "Kullanıcı Grubu Adı:")]
        public string UserGroupName { get; set; } 
    }
}