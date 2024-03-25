using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModel.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Debes Ingresar el Correo Electronico!")]
        [DataType(DataType.Text)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Debes Ingresar la Contraseña!")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
