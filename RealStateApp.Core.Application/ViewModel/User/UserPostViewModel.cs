using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModel.User
{
    public class UserPostViewModel
    {
        public string? UserId { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "El Campo Nombre es Requerido")]
        public string FirstName { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "El Campo Apellido es Requerido")]
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        public string? TypeOfUser { get; set; }
        public string? ImgUrl { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "La Contraseña es Requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "El campo Confirmar Contraseña es Requerido")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Las Contraseñas deben ser Iguales")]
        public string ConfirnPassword { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "El Campo Correo es Requerido")]
        public string Email { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "El Campo Cedula es Requerido")]
        public string Cedula {  get; set; }
        public bool IsActived { get; set; }

    }
}
