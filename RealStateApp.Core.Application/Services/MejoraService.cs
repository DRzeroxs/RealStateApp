using AutoMapper;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.Mejora;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Services
{
    public class MejoraService : GenericServices<MejoraViewModel, SaveMejoraViewModel, Mejora>, IMejoraService
    {
        private readonly IMejoraRepository _repository;
        private readonly IMapper _mapper;
        public MejoraService(IMejoraRepository repository, IMapper mapper) : base(repository, mapper)
        {
             _repository = repository;
            _mapper = mapper;   
        }
    }
}
