using RealStateApp.Core.Domain.Commonts;
using RealStateApp.Core.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Entities
{
    public class Favorita : AuditableBaseEntity
    {
        // Llaves foraneas
        public int PropiedadId { get; set; }
        public int ClienteId { get; set; }

        // Navegadores
        [ForeignKey(nameof(PropiedadId))]
        public Propiedad Propiedad { get; set; }

        [ForeignKey(nameof(ClienteId))]
        public Cliente Cliente { get; set; }
    }
}
