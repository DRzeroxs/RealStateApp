using RealStateApp.Core.Domain.Commonts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Domain.Entities.Users
{
    public class Cliente : PersonBaseEntity
    {
        [InverseProperty(nameof(Cliente))]
        public ICollection<Favorita>  Favorita { get; set; }
    }
}
