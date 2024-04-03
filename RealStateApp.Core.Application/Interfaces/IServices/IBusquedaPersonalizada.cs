using RealStateApp.Core.Application.ViewModel.Propiedad;

namespace RealStateApp.Core.Application.Interfaces.IServices
{
    public interface IBusquedaPersonalizada
    {
        Task<List<PropiedadViewModel>> BuscarPropiedad(string tipoPropiedad, int numeroHabitaciones, int numeroAcedados, int precioMinimo, int precioMaximo);
    }
}