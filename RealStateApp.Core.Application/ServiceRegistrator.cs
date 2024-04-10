using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RealStateApp.Core.Application.Interfaces.IServices;
using RealStateApp.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application
{
    public static class ServiceRegistrator
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IAgenteService, AgenteService>();
            services.AddTransient<ITipoVentaService, TipoVentaService>();
            services.AddTransient<IPropiedadService, PropiedadService>();
            services.AddTransient<IBusquedaPersonalizada, BusquedaPersonalizada>();
            services.AddTransient<ITipoPropiedadService, TipoPropiedadService>();
            services.AddTransient<IPropiedadFavoritaService, PropiedadFavoritaService>();
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IAgenteService, AgenteService>();
            services.AddTransient<IAdministradorService, AdministradorService>();
            services.AddTransient<IMejoraService, MejoraService>();
        }
    }
}
