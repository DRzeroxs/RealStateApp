using RealStateApp.Core.Application.Dto.Agente;
using RealStateApp.Core.Application.ViewModel.AppUsers.Agente;
using RealStateApp.Core.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.IServices
{
    public interface IAgenteService : IGenericServices<AgenteViewModel, SaveAgenteViewModel, Agente>
    {
        Task<AgenteViewModel> GetAgenteByNombre(string nombre);
    }
}
