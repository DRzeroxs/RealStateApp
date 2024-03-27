using AutoMapper;
using Microsoft.VisualBasic;
using RealStateApp.Core.Application.Dto.Propiedades;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.ViewModel.AppUsers.Agente;
using RealStateApp.Core.Application.ViewModel.Mejora;
using RealStateApp.Core.Application.ViewModel.Propiedad;
using RealStateApp.Core.Application.ViewModel.TipoPropiedad;
using RealStateApp.Core.Application.ViewModel.TipoVenta;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Core.Domain.Entities.Descripcion;
using RealStateApp.Core.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Services
{
    public class PropiedadService : GenericServices<PropiedadViewModel, SavePropiedadViewModel, Propiedad>, Interfaces.IServices.IPropiedadService
    {
        private readonly IPropiedadRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAgenteRepository _agenteRepository;
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        private readonly ITipoVentaRepository _tipoVentaRepository;
        private readonly IMejoraRepository _mejoraRepository;
        private readonly IMejorasAplicadasRepository _mejorasAplicadasRepository;
      
        public PropiedadService(IPropiedadRepository repository, IMapper mapper, IAgenteRepository agenteRepository, ITipoPropiedadRepository tipoPropiedadRepository, ITipoVentaRepository tipoVentaRepository, IMejoraRepository mejoraRepository, IMejorasAplicadasRepository mejorasAplicadasRepository) : base(repository, mapper)
        {
             _repository = repository;
            _mapper = mapper;   
            _agenteRepository = agenteRepository;   
            _tipoPropiedadRepository = tipoPropiedadRepository; 
            _tipoVentaRepository = tipoVentaRepository;
            _mejoraRepository = mejoraRepository;   
            _mejorasAplicadasRepository = mejorasAplicadasRepository;  
        }

        #region"GetAllPropiedades"
        public async Task<List<PropiedadViewModel>> GetAllPropiedades()
        {
            var ListPropiedades = await _repository.GetAll();
            var ListAgentes =  await _agenteRepository.GetAll();  
            var ListTipoPropiedad = await _tipoPropiedadRepository.GetAll();
            var ListTipoVenta = await _tipoVentaRepository.GetAll();
            var ListMejoras = await _mejoraRepository.GetAll();
            var ListMejorasAplicadas = await _mejorasAplicadasRepository.GetAll();

            var propiedadesList = from p in ListPropiedades
                                  join a in ListAgentes
                                  on p.AgenteId equals a.Id
                                  select new PropiedadViewModel
                                  {
                                      Id = p.Id,
                                      Identifier = p.Identifier,
                                      Precio = p.Precio,
                                      Size = p.Size,
                                      NumAceados = p.NumAceados,
                                      NumHabitaciones = p.NumHabitaciones,
                                      Descripcion = p.Descripcion,
                                      AgenteId = p.AgenteId,
                                      TipoPropiedad = (from p2 in ListPropiedades
                                                       join tp in ListTipoPropiedad
                                                       on p2.TipoPropiedadId equals tp.Id
                                                       select new TipoPropiedadViewModel
                                                       { Nombre = tp.Nombre, Descripcion = tp.Descripcion, Id = tp.Id }).FirstOrDefault(),


                                      TipoVenta = (from p3 in ListPropiedades
                                                   join tv in ListTipoVenta
                                                   on p3.TipoVentaId equals tv.Id
                                                   select new TipoVentaViewModel { Nombre = tv.Nombre, Id = tv.Id, Descripcion = tv.Descripcion }).FirstOrDefault() ,

                                      Agente = (from p4 in ListPropiedades
                                                join a2 in ListAgentes
                                                on p4.AgenteId equals a.Id
                                                select new AgenteViewModel { Nombre = a.Nombre, Id = a.Id }).FirstOrDefault() ,

                                      Mejoras = (from ma in ListMejorasAplicadas
                                                join p5 in ListPropiedades
                                                on ma.PropiedadId equals p5.Id
                                                join m in ListMejoras
                                                on ma.MejoraId equals m.Id
                                                select new MejoraViewModel
                                                { Nombre = m.Nombre, Descripcion = m.Descripcion }).ToList(),


                                  };
            return propiedadesList.ToList();
        }
        #endregion

        #region"GetAllPropiedadById"
        public async Task<PropiedadViewModel> GetPropiedadesById(int Id)
        {
            var ListPropiedades = await _repository.GetAll();
            var ListAgentes = await _agenteRepository.GetAll();
            var ListTipoPropiedad = await _tipoPropiedadRepository.GetAll();
            var ListTipoVenta = await _tipoVentaRepository.GetAll();
            var ListMejoras = await _mejoraRepository.GetAll();
            var ListMejorasAplicadas = await _mejorasAplicadasRepository.GetAll();

            var propiedad = from p in ListPropiedades
                                  join a in ListAgentes
                                  on p.AgenteId equals a.Id
                                  where p.Id == Id
                                  select new PropiedadViewModel
                                  {
                                      Id = p.Id,
                                      Identifier = p.Identifier,
                                      Precio = p.Precio,
                                      Size = p.Size,
                                      NumAceados = p.NumAceados,
                                      NumHabitaciones = p.NumHabitaciones,
                                      Descripcion = p.Descripcion,
                                      AgenteId = p.AgenteId,
                                      TipoPropiedad = (from p2 in ListPropiedades
                                                       join tp in ListTipoPropiedad
                                                       on p2.TipoPropiedadId equals tp.Id
                                                       select new TipoPropiedadViewModel
                                                       { Nombre = tp.Nombre, Descripcion = tp.Descripcion, Id = tp.Id }).FirstOrDefault(),


                                      TipoVenta = (from p3 in ListPropiedades
                                                   join tv in ListTipoVenta
                                                   on p3.TipoVentaId equals tv.Id
                                                   select new TipoVentaViewModel { Nombre = tv.Nombre, Id = tv.Id, Descripcion = tv.Descripcion }).FirstOrDefault(),

                                      Agente = (from p4 in ListPropiedades
                                                join a2 in ListAgentes
                                                on p4.AgenteId equals a.Id
                                                select new AgenteViewModel { Nombre = a.Nombre, Id = a.Id }).FirstOrDefault(),

                                      Mejoras = (from ma in ListMejorasAplicadas
                                                 join p5 in ListPropiedades
                                                 on ma.PropiedadId equals p5.Id
                                                 join m in ListMejoras
                                                 on ma.MejoraId equals m.Id
                                                 select new MejoraViewModel
                                                 { Nombre = m.Nombre, Descripcion = m.Descripcion }).ToList(),


                                  };
            return propiedad.FirstOrDefault();
        }
        #endregion

        #region "GetAllPropiedadesByCode"
        public async Task<PropiedadViewModel> GetAllPropiedadesByCode(int identifier)
        {
            var ListPropiedades = await _repository.GetAll();
            var ListAgentes = await _agenteRepository.GetAll();
            var ListTipoPropiedad = await _tipoPropiedadRepository.GetAll();
            var ListTipoVenta = await _tipoVentaRepository.GetAll();
            var ListMejoras = await _mejoraRepository.GetAll();
            var ListMejorasAplicadas = await _mejorasAplicadasRepository.GetAll();

            var propiedadesList = from p in ListPropiedades
                                  join a in ListAgentes
                                  on p.AgenteId equals a.Id
                                  where p.Identifier == identifier
                                  select new PropiedadViewModel
                                  {
                                      Id = p.Id,
                                      Identifier = p.Identifier,
                                      Precio = p.Precio,
                                      Size = p.Size,
                                      NumAceados = p.NumAceados,
                                      NumHabitaciones = p.NumHabitaciones,
                                      Descripcion = p.Descripcion,
                                      AgenteId = p.AgenteId,
                                      TipoPropiedad = (from p2 in ListPropiedades
                                                       join tp in ListTipoPropiedad
                                                       on p2.TipoPropiedadId equals tp.Id
                                                       select new TipoPropiedadViewModel
                                                       { Nombre = tp.Nombre, Descripcion = tp.Descripcion, Id = tp.Id }).FirstOrDefault(),


                                      TipoVenta = (from p3 in ListPropiedades
                                                   join tv in ListTipoVenta
                                                   on p3.TipoVentaId equals tv.Id
                                                   select new TipoVentaViewModel { Nombre = tv.Nombre, Id = tv.Id, Descripcion = tv.Descripcion }).FirstOrDefault(),

                                      Agente = (from p4 in ListPropiedades
                                                join a2 in ListAgentes
                                                on p4.AgenteId equals a.Id
                                                select new AgenteViewModel { Nombre = a.Nombre, Id = a.Id }).FirstOrDefault(),

                                      Mejoras = (from ma in ListMejorasAplicadas
                                                 join p5 in ListPropiedades
                                                 on ma.PropiedadId equals p5.Id
                                                 join m in ListMejoras
                                                 on ma.MejoraId equals m.Id
                                                 select new MejoraViewModel
                                                 { Nombre = m.Nombre, Descripcion = m.Descripcion }).ToList(),


                                  };
            return propiedadesList.FirstOrDefault();
        }
        #endregion

    }
}
