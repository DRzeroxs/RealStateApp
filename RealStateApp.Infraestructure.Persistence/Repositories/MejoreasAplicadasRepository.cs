﻿using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infraestructure.Persistence.Repositories
{
    public class MejoreasAplicadasRepository : GenericRepository<MejorasAplicadas>, IMejorasAplicadasRepository
    {
        private readonly ApplicationContext _Context;
        public MejoreasAplicadasRepository(ApplicationContext context) :base(context)
        {
            _Context = context;
        }
    }
}
