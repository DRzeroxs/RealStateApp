using RealStateApp.Core.Application.ViewModel.AppUsers.Administrador;
using RealStateApp.Core.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.IServices
{
    public interface IAdministradorService : IGenericServices<AdministradorViewModel, SaveAdministradorViewModel, Administrador>
    {
    }
}
