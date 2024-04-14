using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModel.User
{
    public class EditUserViewModel
    {
        public string Id { get; set; }  
        [Required(ErrorMessage = "El Nombre es un Campo Requerido.")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "El Apellido es un Campo Requerido.")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "El Numero de Telefono es un Campo Requerido.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string? ImgUrl { get; set; }
        public IFormFile file { get; set; } 
    }
}
