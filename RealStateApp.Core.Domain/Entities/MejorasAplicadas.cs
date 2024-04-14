using RealStateApp.Core.Domain.Commonts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Entities
{
    public class MejorasAplicadas : AuditableBaseEntity
    {
        //Llaves foraneas
        [ForeignKey(nameof(Propiedad))]
        public int PropiedadId { get; set; }
        [ForeignKey(nameof(Mejora))]
        public int MejoraId { get; set; }

        //navegadores
        public ICollection<Propiedad>  Propiedad { get; set; }
        public ICollection<Mejora>  Mejora { get; set; }
    }
}
