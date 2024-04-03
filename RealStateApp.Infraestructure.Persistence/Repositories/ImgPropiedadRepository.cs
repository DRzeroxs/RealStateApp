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
    }
}
