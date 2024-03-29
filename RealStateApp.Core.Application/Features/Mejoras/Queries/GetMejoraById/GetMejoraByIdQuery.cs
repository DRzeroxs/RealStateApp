using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dto.Mejora;
using RealStateApp.Core.Application.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Mejoras.Queries.GetMejoraById
{
    public class GetMejoraByIdQuery : IRequest<MejoraDto>
    {
        public int Id { get; set; }
    }

    public class GetMejoraByIdQueryHandler : IRequestHandler<GetMejoraByIdQuery, MejoraDto>
    {
        private readonly IMejoraRepository _repository;
        private readonly IMapper _mapper;
        public GetMejoraByIdQueryHandler(IMejoraRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MejoraDto> Handle(GetMejoraByIdQuery request, CancellationToken cancellationToken)
        {
            var mejora = await _repository.GetById(request.Id);

            if (mejora == null)
            {
                throw new Exception("No se encontró la mejora");
            }

            return _mapper.Map<MejoraDto>(mejora);
        }
    }
    
}
