using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infraestructure.Persistence.Repositories
{
    public class MejoraRepository : GenericRepository<Mejora>, IMejoraRepository
    {
        //Aqui tambien se manejara el repository para MenjorasAplicadas
        private readonly ApplicationContext _context;
        public MejoraRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
