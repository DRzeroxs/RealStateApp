using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.Mejora;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.ViewModel.Mejora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Mejoras.Queries.GetAllMejoras
{
    public class GetAllMejoraQuery : IRequest<IList<MejoraDto>>
    {

    }

    public class GetAllMejoraQueryHandler : IRequestHandler<GetAllMejoraQuery, IList<MejoraDto>>
    {
        private readonly IMejoraRepository _repository;
        private readonly IMapper _mapper;
        public GetAllMejoraQueryHandler(IMejoraRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<MejoraDto>> Handle(GetAllMejoraQuery request, CancellationToken cancellationToken)
        {
            var mejoras = await _repository.GetAll();

            if (mejoras == null || mejoras.Count == 0)
            {
                throw new Exception("No se encontraron mejoras");
            }

            return _mapper.Map<IList<MejoraDto>>(mejoras);
        }
    }
}
