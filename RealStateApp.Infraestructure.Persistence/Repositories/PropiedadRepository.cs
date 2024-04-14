﻿using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.ViewModel.TipoPropiedad;
using RealStateApp.Core.Application.ViewModel.TipoVenta;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Core.Domain.Entities.Descripcion;
using RealStateApp.Core.Domain.Entities.Users;
using RealStateApp.Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infraestructure.Persistence.Repositories
{
    public class PropiedadRepository : GenericRepository<Propiedad>, IPropiedadRepository
    {
        //Aqui tambien se manejara los repostirios para ImgPropiedad y Favoritas
        private readonly ApplicationContext _context;
        public PropiedadRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<int>> GetIdentificadoresAsync()
        {
            var identificadores = await _context.Set<Propiedad>()
                .Select(x => x.Identifier)
                .ToListAsync();
            return identificadores;
        }

        public async Task<List<Propiedad>> GetAllPropertyByAgentId(int id)
        {
            var propiedad = await _context.Set<Propiedad>()
                .Where(x => x.AgenteId == id)
                .Include(x => x.TipoPropiedad)
                .Include(x => x.TipoVenta)
                .ToListAsync();
            return propiedad;
        }
    }
}
