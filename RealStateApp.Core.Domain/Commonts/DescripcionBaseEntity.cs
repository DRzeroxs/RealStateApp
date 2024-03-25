using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Commonts
{
    public class DescripcionBaseEntity : AuditableBaseEntity
    {
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}
