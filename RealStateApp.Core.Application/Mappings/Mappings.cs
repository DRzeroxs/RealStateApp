using AutoMapper;
using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.Dto.Mejora;
using RealStateApp.Core.Application.Dto.Propiedades;
using RealStateApp.Core.Application.Features.TipoPropiedades.Commands.CreateTipoPropiedad;
using RealStateApp.Core.Application.Features.TipoPropiedades.Commands.UpdateTipoPropiedad;
using RealStateApp.Core.Application.Features.TipoVentas.Commands.CreateTipoVenta;
using RealStateApp.Core.Application.Features.TipoVentas.Commands.UpdateTipoVenta;
using RealStateApp.Core.Application.Features.Mejoras.Commands.CreateMejora;
using RealStateApp.Core.Application.Features.Mejoras.Commands.UpdateMejora;
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
using RealStateApp.Core.Application.Dto.TipoDePropiedad;
using RealStateApp.Core.Application.Dto.TipoVenta;
using RealStateApp.Core.Application.Dto.Agente;

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

            CreateMap<AgenteDto, Agente>()
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

            CreateMap<CreateTipoVentaCommand, TipoVenta>()
                .ForMember(opt => opt.CreatedBy, i => i.Ignore())
                .ForMember(opt => opt.CreatedDate, i => i.Ignore())
                .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
                .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
                .ReverseMap();

            CreateMap<UpdateTipoVentaCommand, TipoVenta>()
                .ForMember(opt => opt.CreatedBy, i => i.Ignore())
                .ForMember(opt => opt.CreatedDate, i => i.Ignore())
                .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
                .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
                .ReverseMap();

            CreateMap<UpdateTipoVentaResponse, TipoVenta>()
               .ForMember(opt => opt.CreatedBy, i => i.Ignore())
               .ForMember(opt => opt.CreatedDate, i => i.Ignore())
               .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
               .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
               .ReverseMap();

            CreateMap<TipoVentaDto, TipoVenta>()
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

            CreateMap<CreateTipoPropiedadCommand,  TipoPropiedad>()
              .ForMember(opt => opt.CreatedBy, i => i.Ignore())
              .ForMember(opt => opt.CreatedDate, i => i.Ignore())
              .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
              .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
              .ReverseMap();

            CreateMap<UpdateTipoPropiedadCommand, TipoPropiedad>()
              .ForMember(opt => opt.CreatedBy, i => i.Ignore())
              .ForMember(opt => opt.CreatedDate, i => i.Ignore())
              .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
              .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
              .ReverseMap();

            CreateMap<UpdateTipoPropiedadResponse, TipoPropiedad>()
             .ForMember(opt => opt.CreatedBy, i => i.Ignore())
             .ForMember(opt => opt.CreatedDate, i => i.Ignore())
             .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
             .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
             .ReverseMap();

            CreateMap<TipoPropiedadDto, TipoPropiedad>()
             .ForMember(opt => opt.CreatedBy, i => i.Ignore())
             .ForMember(opt => opt.CreatedDate, i => i.Ignore())
             .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
             .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
             .ReverseMap();
            #endregion

            #region "Mejoras"

            #region ViewModel

            CreateMap<MejoraViewModel, Mejora>()
            .ForMember(opt => opt.CreatedBy, i => i.Ignore())
            .ForMember(opt => opt.CreatedDate, i => i.Ignore())
            .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
            .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
            .ReverseMap();

            CreateMap<SaveMejoraViewModel, Mejora>()
            .ForMember(opt => opt.CreatedBy, i => i.Ignore())
            .ForMember(opt => opt.CreatedDate, i => i.Ignore())
            .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
            .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
            .ReverseMap();

            #endregion

            #region Dto

            CreateMap<MejoraDto, Mejora>()
           .ForMember(opt => opt.CreatedBy, i => i.Ignore())
           .ForMember(opt => opt.CreatedDate, i => i.Ignore())
           .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
           .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
           .ReverseMap();

            CreateMap<DtoCreateMejora, Mejora>()
            .ForMember(opt => opt.CreatedBy, i => i.Ignore())
            .ForMember(opt => opt.CreatedDate, i => i.Ignore())
            .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
            .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
            .ReverseMap();

            #endregion

            #region Commands

            CreateMap<CreateMejoraCommand, Mejora>()
           .ForMember(opt => opt.CreatedBy, i => i.Ignore())
           .ForMember(opt => opt.CreatedDate, i => i.Ignore())
           .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
           .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
           .ReverseMap();

            CreateMap<UpdateMejoraCommand, Mejora>()
           .ForMember(opt => opt.CreatedBy, i => i.Ignore())
           .ForMember(opt => opt.CreatedDate, i => i.Ignore())
           .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
           .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
           .ReverseMap();

            CreateMap<MejoraUpdateResponse, Mejora>()
           .ForMember(opt => opt.CreatedBy, i => i.Ignore())
           .ForMember(opt => opt.CreatedDate, i => i.Ignore())
           .ForMember(opt => opt.LastModifiedby, i => i.Ignore())
           .ForMember(opt => opt.LastModifiedDate, i => i.Ignore())
           .ReverseMap();

            #endregion

            #endregion
        }
    }
}
