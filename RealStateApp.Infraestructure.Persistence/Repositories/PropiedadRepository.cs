using Microsoft.EntityFrameworkCore;
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


        #region"GetAllPropiedades"
        public async Task<List<Propiedad>> GetAllPropiedades()
        {
            var propiedadesList = from p in _context.Propiedades
                                  join a in _context.Agentes
                                  on p.AgenteId equals a.Id
                                  select new Propiedad
                                  {
                                      Id = p.Id,
                                      Identifier = p.Identifier,
                                      Precio = p.Precio,
                                      Size = p.Size,
                                      NumAceados = p.NumAceados,
                                      NumHabitaciones = p.NumHabitaciones,
                                      Descripcion = p.Descripcion,
                                      AgenteId = p.AgenteId,
                                      TipoPropiedad = (from p2 in _context.Propiedades
                                                       join tp in _context.TiposPropiedad
                                                       on p2.TipoPropiedadId equals tp.Id
                                                       select new TipoPropiedad
                                                       { Nombre = tp.Nombre, Descripcion = tp.Descripcion, Id = tp.Id }).FirstOrDefault() ?? new TipoPropiedad(),


                                      TipoVenta = (from p3 in _context.Propiedades
                                                   join tv in _context.TiposVenta
                                                   on p3.TipoVentaId equals tv.Id
                                                   select new TipoVenta { Nombre = tv.Nombre, Id = tv.Id, Descripcion = tv.Descripcion }).FirstOrDefault() ?? new TipoVenta(),

                                      Agente = (from p4 in _context.Propiedades
                                                join a2 in _context.Agentes
                                                on p4.AgenteId equals a.Id
                                                select new Agente { Nombre = a.Nombre, Id = a.Id}).FirstOrDefault() ?? new Agente(),

                                      //Mejora = (from ma in _context.MejorasAplicadas
                                      //                    join p5 in _context.Propiedades
                                      //                    on ma.PropiedadId equals p5.Id
                                      //                    join m in _context.Mejoras
                                      //                    on ma.MejoraId equals m.Id
                                      //                    select new Mejora
                                      //                    {Nombre = m.Nombre, Descripcion = m.Descripcion}).FirstOrDefault() ?? new Mejora(),


                                  };
            return propiedadesList.ToList();
        }
        #endregion

        #region"GetAllPropiedadById"
        public async Task<Propiedad> GetPropiedadesById(int Id)
        {
            var propiedades = from p in _context.Propiedades
                                  join a in _context.Agentes
                                  on p.AgenteId equals a.Id
                                  where p.Id == Id
                                  select new Propiedad
                                  {
                                      Id = p.Id,
                                      Identifier = p.Identifier,
                                      Precio = p.Precio,
                                      Size = p.Size,
                                      NumAceados = p.NumAceados,
                                      NumHabitaciones = p.NumHabitaciones,
                                      Descripcion = p.Descripcion,
                                      AgenteId = p.AgenteId,
                                      TipoPropiedad = (from p2 in _context.Propiedades
                                                       join tp in _context.TiposPropiedad
                                                       on p2.TipoPropiedadId equals tp.Id
                                                       select new TipoPropiedad
                                                       { Nombre = tp.Nombre, Descripcion = tp.Descripcion, Id = tp.Id }).FirstOrDefault() ?? new TipoPropiedad(),


                                      TipoVenta = (from p3 in _context.Propiedades
                                                   join tv in _context.TiposVenta
                                                   on p3.TipoVentaId equals tv.Id
                                                   select new TipoVenta { Nombre = tv.Nombre, Id = tv.Id, Descripcion = tv.Descripcion }).FirstOrDefault() ?? new TipoVenta(),

                                      Agente = (from p4 in _context.Propiedades
                                                join a2 in _context.Agentes
                                                on p4.AgenteId equals a.Id
                                                select new Agente { Nombre = a.Nombre, Id = a.Id }).FirstOrDefault() ?? new Agente(),

                                      //Mejora = (from ma in _context.MejorasAplicadas
                                      //          join p5 in _context.Propiedades
                                      //          on ma.PropiedadId equals p5.Id
                                      //          join m in _context.Mejoras
                                      //          on ma.MejoraId equals m.Id
                                      //          select new Mejora
                                      //          { Nombre = m.Nombre, Descripcion = m.Descripcion }).FirstOrDefault() ?? new Mejora(),


                                  };
            return propiedades.FirstOrDefault();
        }
        #endregion

        #region "GetAllPropiedadesByCode"
        public async Task<Propiedad> GetAllPropiedadesByCode(int identifier)
        {
            var propiedades = from p in _context.Propiedades
                              join a in _context.Agentes
                              on p.AgenteId equals a.Id
                              where p.Identifier == identifier
                              select new Propiedad
                              {
                                  Id = p.Id,
                                  Identifier = p.Identifier,
                                  Precio = p.Precio,
                                  Size = p.Size,
                                  NumAceados = p.NumAceados,
                                  NumHabitaciones = p.NumHabitaciones,
                                  Descripcion = p.Descripcion,
                                  AgenteId = p.AgenteId,
                                  TipoPropiedad = (from p2 in _context.Propiedades
                                                   join tp in _context.TiposPropiedad
                                                   on p2.TipoPropiedadId equals tp.Id
                                                   select new TipoPropiedad
                                                   { Nombre = tp.Nombre, Descripcion = tp.Descripcion, Id = tp.Id }).FirstOrDefault() ?? new TipoPropiedad(),


                                  TipoVenta = (from p3 in _context.Propiedades
                                               join tv in _context.TiposVenta
                                               on p3.TipoVentaId equals tv.Id
                                               select new TipoVenta { Nombre = tv.Nombre, Id = tv.Id, Descripcion = tv.Descripcion }).FirstOrDefault() ?? new TipoVenta(),

                                  Agente = (from p4 in _context.Propiedades
                                            join a2 in _context.Agentes
                                            on p4.AgenteId equals a.Id
                                            select new Agente { Nombre = a.Nombre, Id = a.Id }).FirstOrDefault() ?? new Agente(),

                                  //Mejora = (from ma in _context.MejorasAplicadas
                                  //          join p5 in _context.Propiedades
                                  //          on ma.PropiedadId equals p5.Id
                                  //          join m in _context.Mejoras
                                  //          on ma.MejoraId equals m.Id
                                  //          select new Mejora
                                  //          { Nombre = m.Nombre, Descripcion = m.Descripcion }).FirstOrDefault() ?? new Mejora(),


                              };
            return propiedades.FirstOrDefault();
        }
        #endregion


    }
}
