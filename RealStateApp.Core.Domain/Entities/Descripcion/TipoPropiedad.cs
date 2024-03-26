using RealStateApp.Core.Domain.Commonts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Entities.Descripcion
{
    public class TipoPropiedad : DescripcionBaseEntity
    {
        //Navegadores
        [InverseProperty(nameof(TipoPropiedad))]
        public Propiedad Propiedad { get; set; }
    }
}
