using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModel.User
{
    public class RegistrerViewModel
    {
        [Required (ErrorMessage = "El Nombre es un Campo Requerido.")]
        [DataType (DataType.Text)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "El Apellido es un Campo Requerido.")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "El Numero de Telefono es un Campo Requerido.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "El Tipo de Usuario es un Campo Requerido.")]
        [DataType(DataType.Text)]
        public string TypeOfUser { get; set; }
        public string? ImgUrl { get; set; }
        [Required(ErrorMessage = "El Nombre de Usuario es un Campo Requerido.")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "La Contraseña es Campo Requerida.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? Cedula { get; set; }

        [Required(ErrorMessage = "Confirmar Contraseña es Campo Requerida.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The  Password must be the same")]
        public string ConfirnPassword { get; set; }
        [Required(ErrorMessage = "La Imagen es Requerida.")]
        [DataType(DataType.EmailAddress)]
        public IFormFile file { get; set; }
        public string Email { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
