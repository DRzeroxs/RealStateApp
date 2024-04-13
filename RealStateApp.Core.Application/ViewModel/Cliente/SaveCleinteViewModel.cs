using RealStateApp.Core.Application.ViewModel.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModel.Cliente
{
    public class SaveCleinteViewModel : BasePersonSaveViewModel
    {
        public string IdentityId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Correo { get; set; }

    }
}
