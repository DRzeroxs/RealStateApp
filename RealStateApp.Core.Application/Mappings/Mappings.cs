using AutoMapper;
using RealStateApp.Core.Application.Dto.Account;
using RealStateApp.Core.Application.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }
    }
}
