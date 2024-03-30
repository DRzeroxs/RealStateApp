using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.TipoPropiedades.Commands.DeleteTipoPropiedadById
{
    public class DeleteTipoPropiedadByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteTipoPropiedadByIdCommandHandler : IRequestHandler<DeleteTipoPropiedadByIdCommand, int>
    {
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        public DeleteTipoPropiedadByIdCommandHandler(ITipoPropiedadRepository tipoPropiedadRepository)
        {
            _tipoPropiedadRepository = tipoPropiedadRepository;
        }

        public async Task<int> Handle(DeleteTipoPropiedadByIdCommand command, CancellationToken cancellationToken)
        {
            var tipoPropiedad = await _tipoPropiedadRepository.GetById(command.Id);
            if (tipoPropiedad == null) throw new Exception("Tipo propiedad not found");
            await _tipoPropiedadRepository.DeleteAsync(tipoPropiedad);
            return tipoPropiedad.Id;
        }
    }
}
