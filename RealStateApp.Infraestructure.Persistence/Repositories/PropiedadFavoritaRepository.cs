using AutoMapper;
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
    public class PropiedadFavoritaRepository : GenericRepository<Favorita>, IPropiedadFavoritaRepository
    {
        private readonly ApplicationContext _Context;
        private readonly IMapper _mapper;
        public PropiedadFavoritaRepository(ApplicationContext context, IMapper mapper) : base(context)
        {
            _Context = context;
        }
    }
}
