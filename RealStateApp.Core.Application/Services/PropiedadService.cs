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
    public class PropiedadService : GenericServices<PropiedadViewModel, SavePropiedadViewModel, Propiedad>, IPropiedadService
    {
        private readonly IPropiedadRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAgenteRepository _agenteRepository;
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        private readonly ITipoVentaRepository _tipoVentaRepository;
        private readonly IMejoraRepository _mejoraRepository;
        private readonly IMejorasAplicadasRepository _mejorasAplicadasRepository;
        private readonly IImgPropieadadRepository _imgPropiedadRepository;
        private readonly IPropiedadFavoritaRepository _propiedadFavoritaRepository;
        private readonly IClienteRepository _clienteRepository;
        private List<Propiedad> _listPropiedades;
        private List<Agente> _listAgentes;
        private List<TipoPropiedad> _listTipoPropiedad;
        private List<TipoVenta> _listTipoVenta;
        private List<Mejora> _listMejoras;
        private List<MejorasAplicadas> _listMejorasAplicadas;
        private List<ImgPropiedad> _listImgPropiedades;


        public PropiedadService(IPropiedadRepository repository, IMapper mapper, IAgenteRepository agenteRepository, ITipoPropiedadRepository tipoPropiedadRepository, ITipoVentaRepository tipoVentaRepository, IMejoraRepository mejoraRepository, IMejorasAplicadasRepository mejorasAplicadasRepository, IImgPropieadadRepository imgPropieadadRepository, IPropiedadFavoritaRepository propiedadFavoritaRepository, IClienteRepository clienteRepository) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _agenteRepository = agenteRepository;
            _tipoPropiedadRepository = tipoPropiedadRepository;
            _tipoVentaRepository = tipoVentaRepository;
            _mejoraRepository = mejoraRepository;
            _mejorasAplicadasRepository = mejorasAplicadasRepository;
            _imgPropiedadRepository = imgPropieadadRepository;
            _propiedadFavoritaRepository = propiedadFavoritaRepository;
            _clienteRepository = clienteRepository;
        }

        private async Task CargarListas()
        {
            _listPropiedades = await _repository.GetAll();
            _listAgentes = await _agenteRepository.GetAll();
            _listTipoPropiedad = await _tipoPropiedadRepository.GetAll();
            _listTipoVenta = await _tipoVentaRepository.GetAll();
            _listMejoras = await _mejoraRepository.GetAll();
            _listMejorasAplicadas = await _mejorasAplicadasRepository.GetAll();
            _listImgPropiedades = await _imgPropiedadRepository.GetAll();
        }

        #region"GetAllPropiedades"
        public async Task<List<PropiedadViewModel>> GetAllPropiedades()
        {

            await CargarListas();

            var propiedadesList = from p in _listPropiedades
                                  join a in _listAgentes
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
                                      TipoPropiedad = (from tp in _listTipoPropiedad
                                                       where tp.Id == p.TipoPropiedadId
                                                       select new TipoPropiedadViewModel
                                                       { Nombre = tp.Nombre, Descripcion = tp.Descripcion, Id = tp.Id }).First(),


                                      TipoVenta = (from p3 in _listPropiedades
                                                   join tv in _listTipoVenta
                                                   on p3.TipoVentaId equals tv.Id
                                                   select new TipoVentaViewModel { Nombre = tv.Nombre, Id = tv.Id, Descripcion = tv.Descripcion }).First(),

                                      Agente = (from p4 in _listPropiedades
                                                join a2 in _listAgentes
                                                on p4.AgenteId equals a.Id
                                                select new AgenteViewModel { Nombre = a.Nombre, Id = a.Id }).First(),

                                      Mejoras = (from ma in _listMejorasAplicadas
                                                 join m in _listMejoras
                                                 on ma.MejoraId equals m.Id
                                                 where ma.PropiedadId == p.Id
                                                 select new MejoraViewModel
                                                 { Nombre = m.Nombre, Descripcion = m.Descripcion }).ToList(),

                                      ImgUrl = (from Img in _listImgPropiedades
                                                where Img.PropieadId == p.Id
                                                select new ImgPropiedadViewModel { UrlImg = Img.UrlImg }).First(),


                                  };
            return propiedadesList.ToList();
        }
        #endregion

        #region"GetAllPropiedadById"
        public async Task<PropiedadViewModel> GetPropiedadesById(int Id)
        {

            await CargarListas();

            var propiedadesList = from p in _listPropiedades
                                  join a in _listAgentes
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
                                      TipoPropiedad = (from tp in _listTipoPropiedad
                                                       where tp.Id == p.TipoPropiedadId
                                                       select new TipoPropiedadViewModel
                                                       { Nombre = tp.Nombre, Descripcion = tp.Descripcion, Id = tp.Id }).First(),


                                      TipoVenta = (from p3 in _listPropiedades
                                                   join tv in _listTipoVenta
                                                   on p3.TipoVentaId equals tv.Id
                                                   select new TipoVentaViewModel { Nombre = tv.Nombre, Id = tv.Id, Descripcion = tv.Descripcion }).First(),

                                      Agente = (from p4 in _listPropiedades
                                                join a2 in _listAgentes
                                                on p4.AgenteId equals a.Id
                                                select new AgenteViewModel { Nombre = a.Nombre, Id = a.Id, ImgUrl = a2.ImgUrl, Telefono = a2.Telefono }).First(),

                                      Mejoras = (from ma in _listMejorasAplicadas
                                                 join m in _listMejoras
                                                 on ma.MejoraId equals m.Id
                                                 where ma.PropiedadId == p.Id
                                                 select new MejoraViewModel
                                                 { Nombre = m.Nombre, Descripcion = m.Descripcion }).ToList(),

                                      ImgUrlList = (from Img in _listImgPropiedades
                                                where Img.PropieadId == p.Id
                                                select new ImgPropiedadViewModel { UrlImg = Img.UrlImg }).ToList(),


                                  };
            return propiedadesList.First();
        }
        #endregion

        #region "GetAllPropiedadesByCode"
        public async Task<PropiedadViewModel> GetAllPropiedadesByCode(int identifier)
        {
            await CargarListas();

            var propiedadesList = from p in _listPropiedades
                                  join a in _listAgentes
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
                                      TipoPropiedad = (from tp in _listTipoPropiedad
                                                       where tp.Id == p.TipoPropiedadId
                                                       select new TipoPropiedadViewModel
                                                       { Nombre = tp.Nombre, Descripcion = tp.Descripcion, Id = tp.Id }).First(),


                                      TipoVenta = (from p3 in _listPropiedades
                                                   join tv in _listTipoVenta
                                                   on p3.TipoVentaId equals tv.Id
                                                   select new TipoVentaViewModel { Nombre = tv.Nombre, Id = tv.Id, Descripcion = tv.Descripcion }).First(),

                                      Agente = (from p4 in _listPropiedades
                                                join a2 in _listAgentes
                                                on p4.AgenteId equals a.Id
                                                select new AgenteViewModel { Nombre = a.Nombre, Id = a.Id }).First(),

                                      Mejoras = (from ma in _listMejorasAplicadas
                                                 join m in _listMejoras
                                                 on ma.MejoraId equals m.Id
                                                 where ma.PropiedadId == p.Id
                                                 select new MejoraViewModel
                                                 { Nombre = m.Nombre, Descripcion = m.Descripcion }).ToList(),

                                      ImgUrl = (from Img in _listImgPropiedades
                                                where Img.PropieadId == p.Id
                                                select new ImgPropiedadViewModel { UrlImg = Img.UrlImg }).First(),


                                  };

            return propiedadesList.First();
        }
        #endregion

        #region "GetPropieadadesPorEspecificaciones"
        public async Task<List<PropiedadViewModel>> GetPropiedadesPorEspecificaciones(string tipoPropiedad,
            int numeroHabitaciones, int numeroAcedados, int precioMinimo, int precioMaximo)
        {
            await CargarListas();

            var propiedades = from p in _listPropiedades
                              join tp in _listTipoPropiedad on p.TipoPropiedadId equals tp.Id
                              where tp.Nombre == tipoPropiedad &&
                                    p.NumHabitaciones == numeroHabitaciones &&
                                    p.NumAceados == numeroAcedados &&
                                    p.Precio >= precioMinimo &&
                                    p.Precio <= precioMaximo
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
                                  TipoPropiedad = (from tp in _listTipoPropiedad
                                                   where tp.Id == p.TipoPropiedadId
                                                   select new TipoPropiedadViewModel
                                                   { Nombre = tp.Nombre, Descripcion = tp.Descripcion, Id = tp.Id }).First(),


                                  TipoVenta = (from p3 in _listPropiedades
                                               join tv in _listTipoVenta
                                               on p3.TipoVentaId equals tv.Id
                                               select new TipoVentaViewModel { Nombre = tv.Nombre, Id = tv.Id, Descripcion = tv.Descripcion }).First(),

                                  Agente = (from p4 in _listPropiedades
                                            join a2 in _listAgentes
                                            on p4.AgenteId equals a2.Id
                                            select new AgenteViewModel { Nombre = a2.Nombre, Id = a2.Id }).First(),

                                  Mejoras = (from ma in _listMejorasAplicadas
                                             join m in _listMejoras
                                             on ma.MejoraId equals m.Id
                                             where ma.PropiedadId == p.Id
                                             select new MejoraViewModel
                                             { Nombre = m.Nombre, Descripcion = m.Descripcion }).ToList(),

                                  ImgUrl = (from Img in _listImgPropiedades
                                            where Img.PropieadId == p.Id
                                            select new ImgPropiedadViewModel { UrlImg = Img.UrlImg }).First(),
                              };

            return propiedades.ToList();
        }
        #endregion

        #region "Busqueda Por Tipo de Propiedad"
        public async Task<List<PropiedadViewModel>> GetPropiedadesPorTipoPropiedad(string tipoPropiedad)
        {
            await CargarListas();

            var propiedades = from p in _listPropiedades
                              join tp in _listTipoPropiedad on p.TipoPropiedadId equals tp.Id
                              where tp.Nombre == tipoPropiedad
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
                                  TipoPropiedad = (from tp in _listTipoPropiedad
                                                   where tp.Id == p.TipoPropiedadId
                                                   select new TipoPropiedadViewModel
                                                   { Nombre = tp.Nombre, Descripcion = tp.Descripcion, Id = tp.Id }).First(),


                                  TipoVenta = (from p3 in _listPropiedades
                                               join tv in _listTipoVenta
                                               on p3.TipoVentaId equals tv.Id
                                               select new TipoVentaViewModel { Nombre = tv.Nombre, Id = tv.Id, Descripcion = tv.Descripcion }).First(),

                                  Agente = (from p4 in _listPropiedades
                                            join a2 in _listAgentes
                                            on p4.AgenteId equals a2.Id
                                            select new AgenteViewModel { Nombre = a2.Nombre, Id = a2.Id }).First(),

                                  Mejoras = (from ma in _listMejorasAplicadas
                                             join m in _listMejoras
                                             on ma.MejoraId equals m.Id
                                             where ma.PropiedadId == p.Id
                                             select new MejoraViewModel
                                             { Nombre = m.Nombre, Descripcion = m.Descripcion }).ToList(),

                                  ImgUrl = (from Img in _listImgPropiedades
                                            where Img.PropieadId == p.Id
                                            select new ImgPropiedadViewModel { UrlImg = Img.UrlImg }).First(),
                              };

            return propiedades.ToList();
        }
        #endregion

        #region "Busqueda Por el Precio Minimo"
        public async Task<List<PropiedadViewModel>> GetPropiedadesPorPrecioMinimo(int precioMinimo)
        {
            await CargarListas();

            var propiedades = from p in _listPropiedades
                              join tp in _listTipoPropiedad on p.TipoPropiedadId equals tp.Id
                              where p.Precio >= precioMinimo
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
                                  TipoPropiedad = (from tp in _listTipoPropiedad
                                                   where tp.Id == p.TipoPropiedadId
                                                   select new TipoPropiedadViewModel
                                                   { Nombre = tp.Nombre, Descripcion = tp.Descripcion, Id = tp.Id }).First(),


                                  TipoVenta = (from p3 in _listPropiedades
                                               join tv in _listTipoVenta
                                               on p3.TipoVentaId equals tv.Id
                                               select new TipoVentaViewModel { Nombre = tv.Nombre, Id = tv.Id, Descripcion = tv.Descripcion }).First(),

                                  Agente = (from p4 in _listPropiedades
                                            join a2 in _listAgentes
                                            on p4.AgenteId equals a2.Id
                                            select new AgenteViewModel { Nombre = a2.Nombre, Id = a2.Id }).First(),

                                  Mejoras = (from ma in _listMejorasAplicadas
                                             join m in _listMejoras
                                             on ma.MejoraId equals m.Id
                                             where ma.PropiedadId == p.Id
                                             select new MejoraViewModel
                                             { Nombre = m.Nombre, Descripcion = m.Descripcion }).ToList(),

                                  ImgUrl = (from Img in _listImgPropiedades
                                            where Img.PropieadId == p.Id
                                            select new ImgPropiedadViewModel { UrlImg = Img.UrlImg }).First(),
                              };

            return propiedades.ToList();
        }
        #endregion

        #region "Busqueda por el Precio Maximo"
        public async Task<List<PropiedadViewModel>> GetPropiedadesPorPrecioMaximo(int precioMaximo)
        {
            await CargarListas();

            var propiedades = from p in _listPropiedades
                              join tp in _listTipoPropiedad on p.TipoPropiedadId equals tp.Id
                              where p.Precio <= precioMaximo
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
                                  TipoPropiedad = (from tp in _listTipoPropiedad
                                                   where tp.Id == p.TipoPropiedadId
                                                   select new TipoPropiedadViewModel
                                                   { Nombre = tp.Nombre, Descripcion = tp.Descripcion, Id = tp.Id }).First(),


                                  TipoVenta = (from p3 in _listPropiedades
                                               join tv in _listTipoVenta
                                               on p3.TipoVentaId equals tv.Id
                                               select new TipoVentaViewModel { Nombre = tv.Nombre, Id = tv.Id, Descripcion = tv.Descripcion }).First(),

                                  Agente = (from p4 in _listPropiedades
                                            join a2 in _listAgentes
                                            on p4.AgenteId equals a2.Id
                                            select new AgenteViewModel { Nombre = a2.Nombre, Id = a2.Id }).First(),

                                  Mejoras = (from ma in _listMejorasAplicadas
                                             join m in _listMejoras
                                             on ma.MejoraId equals m.Id
                                             where ma.PropiedadId == p.Id
                                             select new MejoraViewModel
                                             { Nombre = m.Nombre, Descripcion = m.Descripcion }).ToList(),

                                  ImgUrl = (from Img in _listImgPropiedades
                                            where Img.PropieadId == p.Id
                                            select new ImgPropiedadViewModel { UrlImg = Img.UrlImg }).First(),
                              };

            return propiedades.ToList();
        }
        #endregion

        #region "Buscar Por el Numero de Habitaciones"
        public async Task<List<PropiedadViewModel>> GetPropiedadesPorNumeroHabitaciones(int numeroHabitaciones)
        {
            await CargarListas();

            var propiedades = from p in _listPropiedades
                              join tp in _listTipoPropiedad on p.TipoPropiedadId equals tp.Id
                              where p.NumHabitaciones == numeroHabitaciones
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
                                  TipoPropiedad = (from tp in _listTipoPropiedad
                                                   where tp.Id == p.TipoPropiedadId
                                                   select new TipoPropiedadViewModel
                                                   { Nombre = tp.Nombre, Descripcion = tp.Descripcion, Id = tp.Id }).First(),


                                  TipoVenta = (from p3 in _listPropiedades
                                               join tv in _listTipoVenta
                                               on p3.TipoVentaId equals tv.Id
                                               select new TipoVentaViewModel { Nombre = tv.Nombre, Id = tv.Id, Descripcion = tv.Descripcion }).First(),

                                  Agente = (from p4 in _listPropiedades
                                            join a2 in _listAgentes
                                            on p4.AgenteId equals a2.Id
                                            select new AgenteViewModel { Nombre = a2.Nombre, Id = a2.Id }).First(),

                                  Mejoras = (from ma in _listMejorasAplicadas
                                             join m in _listMejoras
                                             on ma.MejoraId equals m.Id
                                             where ma.PropiedadId == p.Id
                                             select new MejoraViewModel
                                             { Nombre = m.Nombre, Descripcion = m.Descripcion }).ToList(),

                                  ImgUrl = (from Img in _listImgPropiedades
                                            where Img.PropieadId == p.Id
                                            select new ImgPropiedadViewModel { UrlImg = Img.UrlImg }).First(),
                              };

            return propiedades.ToList();
        }
        #endregion

        #region "Buscar por el Numero de Baños"
        public async Task<List<PropiedadViewModel>> GetPropiedadesNumeroBaños(int numeroAceados)
        {
            await CargarListas();

            var propiedades = from p in _listPropiedades
                              join tp in _listTipoPropiedad on p.TipoPropiedadId equals tp.Id
                              where p.NumAceados == numeroAceados
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
                                  TipoPropiedad = (from tp in _listTipoPropiedad
                                                   where tp.Id == p.TipoPropiedadId
                                                   select new TipoPropiedadViewModel
                                                   { Nombre = tp.Nombre, Descripcion = tp.Descripcion, Id = tp.Id }).First(),


                                  TipoVenta = (from p3 in _listPropiedades
                                               join tv in _listTipoVenta
                                               on p3.TipoVentaId equals tv.Id
                                               select new TipoVentaViewModel { Nombre = tv.Nombre, Id = tv.Id, Descripcion = tv.Descripcion }).First(),

                                  Agente = (from p4 in _listPropiedades
                                            join a2 in _listAgentes
                                            on p4.AgenteId equals a2.Id
                                            select new AgenteViewModel { Nombre = a2.Nombre, Id = a2.Id }).First(),

                                  Mejoras = (from ma in _listMejorasAplicadas
                                             join m in _listMejoras
                                             on ma.MejoraId equals m.Id
                                             where ma.PropiedadId == p.Id
                                             select new MejoraViewModel
                                             { Nombre = m.Nombre, Descripcion = m.Descripcion }).ToList(),

                                  ImgUrl = (from Img in _listImgPropiedades
                                            where Img.PropieadId == p.Id
                                            select new ImgPropiedadViewModel { UrlImg = Img.UrlImg }).First(),
                              };

            return propiedades.ToList();
        }
        #endregion

        #region"Buscar por tipo de Propiedad y por el Precio Minimo"
        public async Task<List<PropiedadViewModel>> GetPropiedadesPorTipoPropiedadPrecioMinimo(string tipoPropiedad,
          int precioMinimo)
        {
            await CargarListas();

            var propiedades = from p in _listPropiedades
                              join tp in _listTipoPropiedad on p.TipoPropiedadId equals tp.Id
                              where tp.Nombre == tipoPropiedad &&
                                    p.Precio >= precioMinimo
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
                                  TipoPropiedad = (from tp in _listTipoPropiedad
                                                   where tp.Id == p.TipoPropiedadId
                                                   select new TipoPropiedadViewModel
                                                   { Nombre = tp.Nombre, Descripcion = tp.Descripcion, Id = tp.Id }).First(),


                                  TipoVenta = (from p3 in _listPropiedades
                                               join tv in _listTipoVenta
                                               on p3.TipoVentaId equals tv.Id
                                               select new TipoVentaViewModel { Nombre = tv.Nombre, Id = tv.Id, Descripcion = tv.Descripcion }).First(),

                                  Agente = (from p4 in _listPropiedades
                                            join a2 in _listAgentes
                                            on p4.AgenteId equals a2.Id
                                            select new AgenteViewModel { Nombre = a2.Nombre, Id = a2.Id }).First(),

                                  Mejoras = (from ma in _listMejorasAplicadas
                                             join m in _listMejoras
                                             on ma.MejoraId equals m.Id
                                             where ma.PropiedadId == p.Id
                                             select new MejoraViewModel
                                             { Nombre = m.Nombre, Descripcion = m.Descripcion }).ToList(),

                                  ImgUrl = (from Img in _listImgPropiedades
                                            where Img.PropieadId == p.Id
                                            select new ImgPropiedadViewModel { UrlImg = Img.UrlImg }).First(),
                              };

            return propiedades.ToList();
        }
        #endregion

        #region"Buscar por tipo de Propiedad y el Precio Maximo"
        public async Task<List<PropiedadViewModel>> GetPropiedadesPorTipoPropiedadPrecioMaximo(string tipoPropiedad,
         int precioMaximo)
        {
            await CargarListas();

            var propiedades = from p in _listPropiedades
                              join tp in _listTipoPropiedad on p.TipoPropiedadId equals tp.Id
                              where tp.Nombre == tipoPropiedad &&
                                    p.Precio <= precioMaximo
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
                                  TipoPropiedad = (from tp in _listTipoPropiedad
                                                   where tp.Id == p.TipoPropiedadId
                                                   select new TipoPropiedadViewModel
                                                   { Nombre = tp.Nombre, Descripcion = tp.Descripcion, Id = tp.Id }).First(),


                                  TipoVenta = (from p3 in _listPropiedades
                                               join tv in _listTipoVenta
                                               on p3.TipoVentaId equals tv.Id
                                               select new TipoVentaViewModel { Nombre = tv.Nombre, Id = tv.Id, Descripcion = tv.Descripcion }).First(),

                                  Agente = (from p4 in _listPropiedades
                                            join a2 in _listAgentes
                                            on p4.AgenteId equals a2.Id
                                            select new AgenteViewModel { Nombre = a2.Nombre, Id = a2.Id }).First(),

                                  Mejoras = (from ma in _listMejorasAplicadas
                                             join m in _listMejoras
                                             on ma.MejoraId equals m.Id
                                             where ma.PropiedadId == p.Id
                                             select new MejoraViewModel
                                             { Nombre = m.Nombre, Descripcion = m.Descripcion }).ToList(),

                                  ImgUrl = (from Img in _listImgPropiedades
                                            where Img.PropieadId == p.Id
                                            select new ImgPropiedadViewModel { UrlImg = Img.UrlImg }).First(),
                              };

            return propiedades.ToList();
        }
        #endregion

        #region"Bucar por tipo de Propiedad y por el numero de habitaciones"
        public async Task<List<PropiedadViewModel>> GetPropiedadesPorTipoPropieadNumeroHabitaciones(string tipoPropiedad,
          int numeroHabitaciones)
        {
            await CargarListas();

            var propiedades = from p in _listPropiedades
                              join tp in _listTipoPropiedad on p.TipoPropiedadId equals tp.Id
                              where tp.Nombre == tipoPropiedad &&
                                    p.NumHabitaciones == numeroHabitaciones
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
                                  TipoPropiedad = (from tp in _listTipoPropiedad
                                                   where tp.Id == p.TipoPropiedadId
                                                   select new TipoPropiedadViewModel
                                                   { Nombre = tp.Nombre, Descripcion = tp.Descripcion, Id = tp.Id }).First(),


                                  TipoVenta = (from p3 in _listPropiedades
                                               join tv in _listTipoVenta
                                               on p3.TipoVentaId equals tv.Id
                                               select new TipoVentaViewModel { Nombre = tv.Nombre, Id = tv.Id, Descripcion = tv.Descripcion }).First(),

                                  Agente = (from p4 in _listPropiedades
                                            join a2 in _listAgentes
                                            on p4.AgenteId equals a2.Id
                                            select new AgenteViewModel { Nombre = a2.Nombre, Id = a2.Id }).First(),

                                  Mejoras = (from ma in _listMejorasAplicadas
                                             join m in _listMejoras
                                             on ma.MejoraId equals m.Id
                                             where ma.PropiedadId == p.Id
                                             select new MejoraViewModel
                                             { Nombre = m.Nombre, Descripcion = m.Descripcion }).ToList(),

                                  ImgUrl = (from Img in _listImgPropiedades
                                            where Img.PropieadId == p.Id
                                            select new ImgPropiedadViewModel { UrlImg = Img.UrlImg }).First(),
                              };

            return propiedades.ToList();
        }
        #endregion

        #region "Buscar por el tipo de propiedad y el numero de baños"
        public async Task<List<PropiedadViewModel>> GetPropiedadesPorTipoPropiedadNumeroBaños(string tipoPropiedad,
          int numeroAcedados)
        {
            await CargarListas();

            var propiedades = from p in _listPropiedades
                              join tp in _listTipoPropiedad on p.TipoPropiedadId equals tp.Id
                              where tp.Nombre == tipoPropiedad &&
                                    p.NumAceados == numeroAcedados
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
                                  TipoPropiedad = (from tp in _listTipoPropiedad
                                                   where tp.Id == p.TipoPropiedadId
                                                   select new TipoPropiedadViewModel
                                                   { Nombre = tp.Nombre, Descripcion = tp.Descripcion, Id = tp.Id }).First(),


                                  TipoVenta = (from p3 in _listPropiedades
                                               join tv in _listTipoVenta
                                               on p3.TipoVentaId equals tv.Id
                                               select new TipoVentaViewModel { Nombre = tv.Nombre, Id = tv.Id, Descripcion = tv.Descripcion }).First(),

                                  Agente = (from p4 in _listPropiedades
                                            join a2 in _listAgentes
                                            on p4.AgenteId equals a2.Id
                                            select new AgenteViewModel { Nombre = a2.Nombre, Id = a2.Id }).First(),

                                  Mejoras = (from ma in _listMejorasAplicadas
                                             join m in _listMejoras
                                             on ma.MejoraId equals m.Id
                                             where ma.PropiedadId == p.Id
                                             select new MejoraViewModel
                                             { Nombre = m.Nombre, Descripcion = m.Descripcion }).ToList(),

                                  ImgUrl = (from Img in _listImgPropiedades
                                            where Img.PropieadId == p.Id
                                            select new ImgPropiedadViewModel { UrlImg = Img.UrlImg }).First(),
                              };

            return propiedades.ToList();
        }


        #endregion

        #region"Buscar Propiedades Favoritas"
        public async Task<List<PropiedadViewModel>> GetPropiedadesFavoritas(int Id)
        {

            await CargarListas();
            var propiedadesFavoritasList = await _propiedadFavoritaRepository.GetAll();
            var clienteList = await _clienteRepository.GetAll();


            var propiedadesList = from f in propiedadesFavoritasList
                                  join p in _listPropiedades
                                  on f.PropiedadId equals p.Id
                                  join c in clienteList
                                  on f.ClienteId equals c.Id
                                  where c.Id == Id
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
                                      TipoPropiedad = (from tp in _listTipoPropiedad
                                                       where tp.Id == p.TipoPropiedadId
                                                       select new TipoPropiedadViewModel
                                                       { Nombre = tp.Nombre, Descripcion = tp.Descripcion, Id = tp.Id }).First(),


                                      TipoVenta = (from p3 in _listPropiedades
                                                   join tv in _listTipoVenta
                                                   on p3.TipoVentaId equals tv.Id
                                                   select new TipoVentaViewModel { Nombre = tv.Nombre, Id = tv.Id, Descripcion = tv.Descripcion }).First(),

                                      Agente = (from p4 in _listPropiedades
                                                join a2 in _listAgentes
                                                on p4.AgenteId equals a2.Id
                                                select new AgenteViewModel { Nombre = a2.Nombre, Id = a2.Id }).First(),

                                      Mejoras = (from ma in _listMejorasAplicadas
                                                 join m in _listMejoras
                                                 on ma.MejoraId equals m.Id
                                                 where ma.PropiedadId == p.Id
                                                 select new MejoraViewModel
                                                 { Nombre = m.Nombre, Descripcion = m.Descripcion }).ToList(),

                                      ImgUrl = (from Img in _listImgPropiedades
                                                where Img.PropieadId == p.Id
                                                select new ImgPropiedadViewModel { UrlImg = Img.UrlImg }).First(),


                                  };
            return propiedadesList.ToList();
        }
        #endregion

        #region"Contar Propieadades"
        public async Task <int> ContarPropieades()
        {
            var propiedades = await _propiedadFavoritaRepository.GetAll();  
            var count = propiedades.ToList().Count();

            return count;
        }
        #endregion

    }
}
