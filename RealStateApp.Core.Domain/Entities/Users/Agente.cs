using RealStateApp.Core.Domain.Commonts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Entities.Users
{
    public class Agente : PersonBaseEntity
    {
        public string? ImgUrl { get; set; }

        [InverseProperty(nameof(Agente))]
        public Propiedad Propiedad { get; set; }
    }
}
