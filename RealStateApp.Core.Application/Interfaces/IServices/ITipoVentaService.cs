using RealStateApp.Core.Application.ViewModel.TipoVenta;
using RealStateApp.Core.Domain.Entities.Descripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.IServices
{
    public interface ITipoVentaService : IGenericServices<TipoVentaViewModel, SaveTipoVentaViewModel, TipoVenta>
    {
        Task<List<TipoVentaViewModel>> GetTipoVentasAsync();
    }
}
