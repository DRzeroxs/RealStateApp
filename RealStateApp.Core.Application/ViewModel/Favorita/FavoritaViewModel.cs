using RealStateApp.Core.Application.ViewModel.AppUsers.Cliente;
using RealStateApp.Core.Application.ViewModel.Propiedad;
using RealStateApp.Core.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModel.Favorita
{
    public class FavoritaViewModel
    {
        public int PropiedadId { get; set; }
 
        public int ClienteId { get; set; }

        //Navegadores
        public PropiedadViewModel Propiedad { get; set; }
        public ClienteViewModel Cliente { get; set; }
    }
}
