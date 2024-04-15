using RealStateApp.Core.Domain.Commonts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Entities.Descripcion
{
    public class ImgPropiedad : AuditableBaseEntity
    {
        public string UrlImg { get; set; }

        // Llave foranea
        public int PropieadId { get; set; }

        // Navegador
        public Propiedad Propiedad { get; set; }
    }
}
