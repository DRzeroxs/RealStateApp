using RealStateApp.Core.Domain.Commonts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Entities
{
    public class Mejora : DescripcionBaseEntity
    {
        public ICollection<MejorasAplicadas> MejorasAplicadas { get; set; }
    }
}
