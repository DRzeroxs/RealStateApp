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
        //Llaves foraneas
        [ForeignKey(nameof(Propiedad))]
        public int PropiedadId { get; set; }
        [ForeignKey(nameof(Cliente))]
        public int ClienteId { get; set; }  

        //Navegadores
        public Propiedad Propiedad { get; set; }
        public Cliente Cliente { get; set;}
    }
}
