using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MegProject.Data.Models;

namespace MegProject.Dto.CustomDto.ViewModels
{
    public class PermissionRegisterViewModel
    {
        [Required(ErrorMessage = "Boş Bırakılamaz")]
        [Display(Name = "İzin Adı")]
        public string PermissionName { get; set; } 

        [Required(ErrorMessage = "Boş Bırakılamaz")]
        [Display(Name = "İzin Sayfa Ve Methodları")]
        public ICollection<SystemControllers> ControllersActions { get; set; } 
    }
}