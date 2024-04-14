using Microsoft.EntityFrameworkCore;
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
    public class ImgPropiedadRepository : GenericRepository<ImgPropiedad>, IImgPropieadadRepository
    {
        private readonly ApplicationContext _Context;
        public ImgPropiedadRepository(ApplicationContext context) :base(context)
        {
            _Context = context;
        }

        public async Task<List<ImgPropiedad>> GetImgPropiedadByPropiedadId(int PropiedadId)
        {
            var result = await _Context.ImgPropiedades.Where(x => x.PropieadId == PropiedadId).ToListAsync();
            return result;
        }
    }
}
