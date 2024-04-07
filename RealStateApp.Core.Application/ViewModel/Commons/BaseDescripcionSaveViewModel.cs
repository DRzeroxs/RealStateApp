using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModel.Commons
{
    public abstract class BaseDescripcionSaveViewModel
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo Descripcion es requerido")]
        [DataType(DataType.Text)]
        public string? Descripcion { get; set; }
    }
}
