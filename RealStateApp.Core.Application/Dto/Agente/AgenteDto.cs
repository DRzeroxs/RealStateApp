using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Dto.Agente
{
    public class AgenteDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public int CantidadPropiedades { get; set; }


    }
}
