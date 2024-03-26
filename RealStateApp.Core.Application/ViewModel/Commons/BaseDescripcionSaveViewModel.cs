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
        public string Nombre { get; set; }

        public string? Descripcion { get; set; }
    }
}
