using RealStateApp.Core.Application.ViewModel.AppUsers.Agente;
using RealStateApp.Core.Application.ViewModel.Mejora;
using RealStateApp.Core.Application.ViewModel.TipoPropiedad;
using RealStateApp.Core.Application.ViewModel.TipoVenta;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModel.Propiedad
{
    public class PropiedadViewModel
    {
        public int Id { get; set; }
        public int Identifier { get; set; }

        public string Size { get; set; }

        public double Precio { get; set; }

        public string Descripcion { get; set; }

        public int NumHabitaciones { get; set; }

        public int NumAceados { get; set; }

        public int TipoPropiedadId { get; set; }

        //eliminar
        public ImgPropiedadViewModel ImgUrl {  get; set; }
        public List<ImgPropiedadViewModel> ImgUrlList { get; set; }
        public int TipoVentaId { get; set; }
        public int AgenteId { get; set; }
        public string NombreAgente {  get; set; }   
        public string TipoPropiedadNombre { get; set; }
        public List<MejoraViewModel> Mejoras { get; set; }
        public TipoPropiedadViewModel TipoPropiedad { get; set; }
        public TipoVentaViewModel TipoVenta { get; set; }
        public AgenteViewModel Agente { get; set; }
    }
}
