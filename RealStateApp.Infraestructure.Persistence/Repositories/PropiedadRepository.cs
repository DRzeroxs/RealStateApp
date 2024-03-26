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
    public class PropiedadRepository : GenericRepository<Propiedad>, IPropiedadRepository
    {
        //Aqui tambien se manejara los repostirios para ImgPropiedad y Favoritas
        private readonly ApplicationContext _context;
        public PropiedadRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
