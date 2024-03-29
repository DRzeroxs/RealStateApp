using AutoMapper;
using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.Dto.Propiedades;
using RealStateApp.Core.Application.ViewModel.AppUsers.Agente;
using RealStateApp.Core.Application.ViewModel.Mejora;
using RealStateApp.Core.Application.ViewModel.Propiedad;
using RealStateApp.Core.Application.ViewModel.TipoPropiedad;
using RealStateApp.Core.Application.ViewModel.TipoVenta;
using RealStateApp.Core.Application.ViewModel.User;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Core.Domain.Entities.Descripcion;
using RealStateApp.Core.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Mappings
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            #region "User"
            CreateMap<RegistrerViewModel, RegistrerRequest>().ReverseMap()
                .ForMember(opt => opt.Error, i => i.Ignore())
                .ForMember(opt => opt.HasError, i => i.Ignore());

            CreateMap<LoginViewModel, AuthenticationRequest>().ReverseMap()
               .ForMember(opt => opt.Error, i => i.Ignore())
               .ForMember(opt => opt.HasError, i => i.Ignore());
            #endregion

            #region "Propiedades"
            CreateMap<PropiedadViewModel, Propiedad>()
              .ForMember(opt => opt.CreatedBy, i => i.Ignore())
              .ForMember(opt => opt.CreatedDate, i => i.Ignore())
              .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
               .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
              .ReverseMap()
              .ForMember(opt => opt.ImgUrl, i => i.Ignore())
                .ForMember(opt => opt.ImgUrlList, i => i.Ignore());

            CreateMap<PropiedadesDto, Propiedad>()
             .ForMember(opt => opt.CreatedBy, i => i.Ignore())
             .ForMember(opt => opt.CreatedDate, i => i.Ignore())
             .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
              .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
             .ReverseMap();

            CreateMap<PropiedadesDto, PropiedadViewModel>()
                .ForMember(opt => opt.ImgUrl, i => i.Ignore())
                .ForMember(opt => opt.ImgUrlList, i => i.Ignore())
            .ReverseMap();

            CreateMap<ImgPropiedadViewModel,ImgPropiedad >()
              .ForMember(opt => opt.CreatedBy, i => i.Ignore())
             .ForMember(opt => opt.CreatedDate, i => i.Ignore())
             .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
              .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
             .ReverseMap();
            #endregion

            #region "Agente"
            CreateMap<AgenteViewModel, Agente>()
            .ForMember(opt => opt.CreatedBy, i => i.Ignore())
            .ForMember(opt => opt.CreatedDate, i => i.Ignore())
            .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
             .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
            .ReverseMap();

            CreateMap<SaveAgenteViewModel, Agente>()
           .ForMember(opt => opt.CreatedBy, i => i.Ignore())
           .ForMember(opt => opt.CreatedDate, i => i.Ignore())
           .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
            .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
           .ReverseMap();
            #endregion

            #region"Tipo de Venta"
            CreateMap<TipoVentaViewModel, TipoVenta>()
           .ForMember(opt => opt.CreatedBy, i => i.Ignore())
           .ForMember(opt => opt.CreatedDate, i => i.Ignore())
           .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
            .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
           .ReverseMap();

            CreateMap<SaveTipoVentaViewModel, TipoVenta>()
           .ForMember(opt => opt.CreatedBy, i => i.Ignore())
           .ForMember(opt => opt.CreatedDate, i => i.Ignore())
           .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
            .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
           .ReverseMap();
            #endregion

            #region "Tipo de Propiedades"
            CreateMap<TipoPropiedadViewModel, TipoPropiedad>()
           .ForMember(opt => opt.CreatedBy, i => i.Ignore())
           .ForMember(opt => opt.CreatedDate, i => i.Ignore())
           .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
            .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
           .ReverseMap();

            CreateMap<SaveTipoPropiedadViewModel, TipoPropiedad>()
          .ForMember(opt => opt.CreatedBy, i => i.Ignore())
          .ForMember(opt => opt.CreatedDate, i => i.Ignore())
          .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
           .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
          .ReverseMap();
            #endregion

            #region "Mejoras"
            CreateMap<MejoraViewModel, Mejora>()
            .ForMember(opt => opt.CreatedBy, i => i.Ignore())
            .ForMember(opt => opt.CreatedDate, i => i.Ignore())
            .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
            .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
            .ReverseMap();

            #endregion
        }
    }
}
