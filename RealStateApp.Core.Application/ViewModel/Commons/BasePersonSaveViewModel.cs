using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModel.Commons
{
    public abstract class BasePersonSaveViewModel
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }

        [Required(ErrorMessage = "El campo Correo es requerido")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Apellido es requerido")]
        [DataType(DataType.Text)]
        public string Apellido { get; set; }

        [MaxLength(11, ErrorMessage ="Ingrese una Cedula Valida")]
        [MinLength(11, ErrorMessage ="Ingrese una Cedula Valida")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "La cédula solo puede contener números")]
        [Required(ErrorMessage = "El campo Cedula es requerido")]
        [DataType(DataType.Text)]
        public string Cedula { get; set; }
    }
}
