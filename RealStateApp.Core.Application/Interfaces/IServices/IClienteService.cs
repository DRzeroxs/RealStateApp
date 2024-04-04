using RealStateApp.Core.Application.ViewModel.AppUsers.Cliente;
using RealStateApp.Core.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.IServices
{
    public interface IClienteService : IGenericServices<ClienteViewModel, SaveClienteViewModel, Cliente>
    {
        Task<ClienteViewModel> GetClientePorIdentityId(string Identity);
    }
}
