using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Commonts
{
    public class PersonBaseEntity : AuditableBaseEntity
    {
        public string IdentityId { get; set; }

        public string Correo { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }

        [MaxLength(11)]
        public string? Cedula { get; set; }

    }
}
