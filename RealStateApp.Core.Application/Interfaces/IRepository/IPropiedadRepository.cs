﻿using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.IRepository
{
    public interface IPropiedadRepository : IGenericRepository<Propiedad>
    {
        Task<List<Propiedad>> GetAllPropertyByAgentId(int id);
        Task<List<int>> GetIdentificadoresAsync();
    }
}
