using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities.Descripcion;
using RealStateApp.Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infraestructure.Persistence.Repositories
{
    public class TipoPropiedadRepository : GenericRepository<TipoPropiedad>, ITipoPropiedadRepository
    {
        private readonly ApplicationContext _context;
        public TipoPropiedadRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<TipoPropiedad>> GetAllPropertyNameByAgentId(int id)
        {
            var tipoPropiedad =  _context.Set<TipoPropiedad>().Where(x => x.Propiedad.AgenteId == id).ToList();
            return tipoPropiedad;
        }
    }
}
