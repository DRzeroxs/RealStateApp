using RealStateApp.Core.Application.ViewModel.Commons;
using RealStateApp.Core.Application.ViewModel.Propiedad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModel.AppUsers.Cliente
{
    public class ClienteViewModel : BasePersonViewModel
    {
        List<PropiedadViewModel>? Favoritas { get; set; }
    }
}
