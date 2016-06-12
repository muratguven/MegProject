using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MegProject.Data.Models;

namespace MegProject.Dto.CustomDto.ViewModels
{
    [DataContract]
    [Serializable]
    public class RoleRegisterViewModel
    {
        [DataMember]
        [Display(Name = "Rol Adı")]        
         public string RoleName { get; set; }
         
        [DataMember]
        [Display(Name = "Ana Sayfalar")]
         public List<SystemControllers> Controllers { get; set; } 
        
        
    }
}