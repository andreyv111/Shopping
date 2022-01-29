using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopping.Models
{
    public class Login
    {
        [Required]
        [Display(Name = "Usuario:")]
        public string Usuario { set; get; }

        [Required]
        [Display(Name = "Contraseña:")]
        public string password { set; get; }
    }
}