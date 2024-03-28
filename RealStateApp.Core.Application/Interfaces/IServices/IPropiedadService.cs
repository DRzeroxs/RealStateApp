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
        #region"Consultas"
        Task<List<PropiedadViewModel>> GetAllPropiedades();
        Task<PropiedadViewModel> GetPropiedadesById(int Id);
        Task<List<PropiedadViewModel>> GetAllPropiedadesByCode(int identifier);
        Task<List<PropiedadViewModel>> GetPropiedadesPorEspecificaciones(string tipoPropiedad,
            int numeroHabitaciones, int numeroAcedados, int precioMinimo, int precioMaximo);
        Task<List<PropiedadViewModel>> GetPropiedadesPorTipoPropiedad(string tipoPropiedad);
        Task<List<PropiedadViewModel>> GetPropiedadesPorPrecioMinimo(int precioMinimo);
        Task<List<PropiedadViewModel>> GetPropiedadesPorPrecioMaximo(int precioMaximo);
        Task<List<PropiedadViewModel>> GetPropiedadesPorNumeroHabitaciones(int numeroHabitaciones);
        Task<List<PropiedadViewModel>> GetPropiedadesNumeroBaños(int numeroAceados);
        Task<List<PropiedadViewModel>> GetPropiedadesPorTipoPropiedadPrecioMinimo(string tipoPropiedad,
          int precioMinimo);
        Task<List<PropiedadViewModel>> GetPropiedadesPorTipoPropiedadPrecioMaximo(string tipoPropiedad,
         int precioMaximo);
        Task<List<PropiedadViewModel>> GetPropiedadesPorTipoPropieadNumeroHabitaciones(string tipoPropiedad,
          int numeroHabitaciones);
        Task<List<PropiedadViewModel>> GetPropiedadesPorTipoPropiedadNumeroBaños(string tipoPropiedad,
          int numeroAcedados);

        #endregion
    }
}
