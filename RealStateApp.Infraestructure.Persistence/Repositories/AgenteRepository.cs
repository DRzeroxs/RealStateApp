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
    public class AgenteRepository : GenericRepository<Agente>, IAgenteRepository
    {
        private readonly ApplicationContext _context;
        public AgenteRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
