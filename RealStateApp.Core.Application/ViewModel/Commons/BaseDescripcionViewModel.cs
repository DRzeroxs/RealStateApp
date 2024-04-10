using RealStateApp.Core.Application.ViewModel.Propiedad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModel.Commons
{
    public abstract class BaseDescripcionViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int CountPropiedades { get; set; }

        List<PropiedadViewModel>? Propiedades { get; set;}
    }
}
