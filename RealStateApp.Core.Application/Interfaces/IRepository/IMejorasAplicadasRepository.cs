using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.IRepository
{
    public interface IMejorasAplicadasRepository : IGenericRepository<MejorasAplicadas>
    {
        Task<List<MejorasAplicadas>> GetMejorasAplicadasByPropiedadId(int PropiedadId);

        Task AgregarMejorasdePropiedad(int PropiedadId, List<int> MejoraId);
    }
}
