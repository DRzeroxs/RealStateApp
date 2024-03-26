using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities.Users;
using RealStateApp.Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infraestructure.Persistence.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>, IClientesRepository
    {
        private readonly ApplicationContext _context;

        public ClienteRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
