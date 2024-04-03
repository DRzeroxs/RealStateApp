using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Domain.Entities.Descripcion;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealStateApp.Core.Application.ViewModel.Propiedad;

namespace RealStateApp.Core.Application.Services
{
    public class BusquedaPersonalizada : IBusquedaPersonalizada
    {
        private readonly IPropiedadService _propiedadesService;
        public BusquedaPersonalizada(IPropiedadService service)
        {
            _propiedadesService = service;
        }

        public async Task<List<PropiedadViewModel>> BuscarPropiedad(string tipoPropiedad,
            int numeroHabitaciones, int numeroAcedados, int precioMinimo, int precioMaximo)
        {
            var propiedades = new List<PropiedadViewModel>();

            #region"Consultas"

            if (tipoPropiedad is not null && numeroHabitaciones is not 0 && numeroAcedados is not 0
               && precioMinimo is not 0 && precioMaximo is not 0)
            {
                propiedades = await _propiedadesService.GetPropiedadesPorEspecificaciones(tipoPropiedad,
                numeroHabitaciones, numeroAcedados, precioMinimo, precioMaximo);
            }

            else if (tipoPropiedad is not null)
            {
                propiedades = await _propiedadesService.GetPropiedadesPorTipoPropiedad(tipoPropiedad);
            }

            else if (precioMinimo is not 0)
            {
                propiedades = await _propiedadesService.GetPropiedadesPorPrecioMinimo(precioMinimo);
            }

            else if (precioMaximo is not 0)
            {
                propiedades = await _propiedadesService.GetPropiedadesPorPrecioMaximo(precioMaximo);
            }

            else if (numeroHabitaciones is not 0)
            {
                propiedades = await _propiedadesService.GetPropiedadesPorNumeroHabitaciones(numeroHabitaciones);
            }

            else if (numeroAcedados is not 0)
            {
                propiedades = await _propiedadesService.GetPropiedadesNumeroBaños(numeroAcedados);
            }
            else
            {
                propiedades = await _propiedadesService.GetAllPropiedades();
            }
            return propiedades;

            #endregion
        }

    }
}
