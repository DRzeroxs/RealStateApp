using Microsoft.EntityFrameworkCore;
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
    public class MejoreasAplicadasRepository : GenericRepository<MejorasAplicadas>, IMejorasAplicadasRepository
    {
        private readonly ApplicationContext _Context;
        public MejoreasAplicadasRepository(ApplicationContext context) :base(context)
        {
            _Context = context;
        }

        public async Task<List<MejorasAplicadas>> GetMejorasAplicadasByPropiedadId(int PropiedadId)
        {
            var result = await _Context.MejorasAplicadas.Where(x => x.PropiedadId == PropiedadId).ToListAsync();
            return result;
        }

        public async Task AgregarMejorasdePropiedad(int PropiedadId, List<int> MejoraId)
        {
            foreach (var Id in MejoraId)
            {
                MejorasAplicadas mejora = new MejorasAplicadas();

                mejora.MejoraId = Id;
                mejora.PropiedadId = PropiedadId;

                await _Context.MejorasAplicadas.AddAsync(mejora);

                await _Context.SaveChangesAsync();
            }
        }


    }
}
