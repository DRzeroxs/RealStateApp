using RealStateApp.Core.Application.Dto.Propiedades;
using RealStateApp.Core.Application.ViewModel.Propiedad;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.IServices
{
    public interface IPropiedadService : IGenericServices<PropiedadViewModel, SavePropiedadViewModel, Propiedad>
    {
        Task<List<PropiedadViewModel>> GetAllPropiedades();
        Task<PropiedadViewModel> GetPropiedadesById(int Id);
        Task<PropiedadViewModel> GetAllPropiedadesByCode(int identifier);
    }
}
