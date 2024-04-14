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
        // Llaves foraneas
        public int PropiedadId { get; set; }
        public int MejoraId { get; set; }

        // Navegadores
        [ForeignKey(nameof(PropiedadId))]
        public Propiedad Propiedad { get; set; }

        [ForeignKey(nameof(MejoraId))]
        public Mejora Mejora { get; set; }
    }
}
