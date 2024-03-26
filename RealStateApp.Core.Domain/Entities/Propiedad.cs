using RealStateApp.Core.Domain.Commonts;
using RealStateApp.Core.Domain.Entities.Descripcion;
using RealStateApp.Core.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Entities
{
    public class Propiedad : AuditableBaseEntity
    {
        public int Identifier { get; set; }

        public string Size { get; set; }
        public double Precio { get; set; }
        public string? Descripcion { get; set; }
        public int NumHabitaciones { get; set; }
        public int NumAceados { get; set; }

        //LLaves foraneas
        [ForeignKey(nameof(TipoPropiedad))]
        public int TipoPropiedadId { get; set; }

        [ForeignKey(nameof(TipoVenta))]
        public int TipoVentaId { get; set; }

        [ForeignKey(nameof(Agente))]
        public int AgenteId { get; set; }

        //Navegadores
        public TipoPropiedad TipoPropiedad { get; set; }
        public TipoVenta TipoVenta { get; set;}
        public Agente Agente { get; set; }
        public Mejora Mejora { get; set; }  

        [InverseProperty(nameof(Propiedad))]
        public ImgPropiedad ImgPropiedad { get; set;}
        
        [InverseProperty(nameof(Propiedad))]
        public MejorasAplicadas MejorasAplicadas { get; set;}
        
        [InverseProperty(nameof(Propiedad))]
        public Favorita Favorita { get; set;}
    }
}
