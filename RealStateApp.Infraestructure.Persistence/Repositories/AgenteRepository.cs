using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Dto.Agente;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities;
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

        public override async Task<Agente> GetById(int id)
        {
            var agente = await _context.Set<Agente>().Include(x => x.Propiedad).ToListAsync();
            return agente.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Agente> GetPropiedadByAgenteId(int id)
        {
            var agente = await _context.Set<Agente>()
                .Include(x => x.Propiedad)
                .Include(x => x.Propiedad.TipoPropiedad)
                .Include(x => x.Propiedad.TipoVenta)
                .Include(x => x.Propiedad.MejorasAplicadas.Mejora)
                .ToListAsync();
            return agente.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Agente> GetAgenteByNombre(string nombre)
        {
            var agente = await _context.Set<Agente>()
                .Include(x => x.Propiedad)
                .Include(x => x.Propiedad.TipoPropiedad)
                .Include(x => x.Propiedad.TipoVenta)
                .FirstOrDefaultAsync(x => x.Nombre == nombre);
            return agente;
        }
        public async Task<int> GetCantidadPropiedadAgenteById(int id)
        {
            var agente = await _context.Set<Agente>().Where(x => x.Id == id).Include(x => x.Propiedad).CountAsync();
            return agente;
        }

        public async Task<int> GetCantidadPropiedadAgente()
        {
            var agente = await _context.Set<Agente>().Include(x => x.Propiedad).CountAsync();
            return agente;
        }

    }
}
