using RealStateApp.Core.Application.Services;
using RealStateApp.Core.Application.ViewModel.Favorita;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.IServices
{
    public interface IPropiedadFavoritaService : IGenericServices<FavoritaViewModel, SaveFavoritaViewModel, Favorita>
    {
    }
}
