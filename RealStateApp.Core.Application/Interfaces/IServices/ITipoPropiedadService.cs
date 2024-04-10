﻿using RealStateApp.Core.Application.ViewModel.TipoPropiedad;
using RealStateApp.Core.Domain.Entities.Descripcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.IServices
{
    public interface ITipoPropiedadService : IGenericServices<TipoPropiedadViewModel, SaveTipoPropiedadViewModel, TipoPropiedad>
    {
        Task<List<TipoPropiedadViewModel>> GetTipoPropiedadAsync();
    }
}
