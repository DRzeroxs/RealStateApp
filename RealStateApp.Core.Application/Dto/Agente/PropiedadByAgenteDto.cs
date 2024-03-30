using RealStateApp.Core.Application.ViewModel.AppUsers.Agente;
using RealStateApp.Core.Application.ViewModel.Mejora;
using RealStateApp.Core.Application.ViewModel.Propiedad;
using RealStateApp.Core.Application.ViewModel.TipoPropiedad;
using RealStateApp.Core.Application.ViewModel.TipoVenta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Dto.Agente
{
    public class PropiedadByAgenteDto
    {
        public int Id { get; set; }
        public int? Identifier { get; set; }
        public string Size { get; set; }
        public double Precio { get; set; }
        public string Descripcion { get; set; }
        public int NumHabitaciones { get; set; }
        public int NumAceados { get; set; }
        public TipoPropiedadViewModel TipoPropiedad { get; set; }
        public TipoVentaViewModel TipoVenta { get; set; }
    }
}
