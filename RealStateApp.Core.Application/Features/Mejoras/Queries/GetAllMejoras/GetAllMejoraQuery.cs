using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.Mejora;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.ViewModel.Mejora;
using RealStateApp.Core.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Mejoras.Queries.GetAllMejoras
{
    //<summary>
    //Optener todas las mejoras
    //</summary>
    public class GetAllMejoraQuery : IRequest<Response<IList<MejoraDto>>>
    {

    }

    public class GetAllMejoraQueryHandler : IRequestHandler<GetAllMejoraQuery, Response<IList<MejoraDto>>>
    {
        private readonly IMejoraRepository _repository;
        private readonly IMapper _mapper;
        public GetAllMejoraQueryHandler(IMejoraRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IList<MejoraDto>>> Handle(GetAllMejoraQuery request, CancellationToken cancellationToken)
        {
            var mejoras = await _repository.GetAll();

            if (mejoras == null || mejoras.Count == 0)
            {
                throw new ApiExeption("No se encontro ninguna mejora", (int)HttpStatusCode.NoContent);
            }

            return new Response<IList<MejoraDto>>(_mapper.Map<IList<MejoraDto>>(mejoras));
        }
    }
}
