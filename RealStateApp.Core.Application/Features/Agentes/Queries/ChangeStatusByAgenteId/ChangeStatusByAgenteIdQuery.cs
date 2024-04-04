using MediatR;
using RealStateApp.Core.Application.Dto.Agente;
using RealStateApp.Core.Application.Interfaces.IAccount;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Agentes.Queries.ChangeStatusByAgenteId
{
    // <summary>
    // Cambiar el estado de un agente por el id
    // </summary>
    public class ChangeStatusByAgenteIdQuery : IRequest<Response<bool>>
    {
        [SwaggerParameter(Description = "Id del agente que desea cambiarle el estado")]
        public string Id { get; set; }

        [SwaggerParameter(Description = "Estado del agente al que desea cambiar si es true o false")]
        public bool Status { get; set; }
    }

    public class ChangeStatusByAgenteIdQueryHandler : IRequestHandler<ChangeStatusByAgenteIdQuery, Response<bool>>
    {
        private readonly IAccountServiceApi _accountServiceApi;
        public ChangeStatusByAgenteIdQueryHandler(IAccountServiceApi accountServiceApi)
        {
            _accountServiceApi = accountServiceApi;
        }

        public async Task<Response<bool>> Handle(ChangeStatusByAgenteIdQuery request, CancellationToken cancellationToken)
        {
            var changeStatus = await ChangeStatus(request.Id, request.Status);
            return new Response<bool>(changeStatus);
        }

        private async Task<bool> ChangeStatus(string Id, bool status)
        {
            if (status == true)
            {
                var active = await _accountServiceApi.ActiveAccountAsync(Id);
                return active;
            }
            else
            {
                var inactive = await _accountServiceApi.InactiveAccountAsync(Id);
                return inactive;
            }
        }
    }
}
