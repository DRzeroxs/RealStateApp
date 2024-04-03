using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Infraestructure.Persistence.Context;
using RealStateApp.Infraestructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infraestructure.Persistence
{
    public static class ServicesRegistrator
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context
            services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("conexion"),
                            m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)
                    )
                );
            #endregion

            #region "Services"
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IAgenteRepository, AgenteRepository>();
            services.AddScoped<IAdministradorRepository, AdministradorRepository>();

            services.AddScoped<IPropiedadRepository, PropiedadRepository>();
            services.AddScoped<IMejoraRepository, MejoraRepository>();

            services.AddScoped<ITipoPropiedadRepository, TipoPropiedadRepository>();
            services.AddScoped<ITipoVentaRepository, TipoVentaRepository>();

            services.AddTransient<IMejorasAplicadasRepository, MejoreasAplicadasRepository>();
            services.AddTransient<IImgPropieadadRepository, ImgPropiedadRepository>();
            #endregion
        }
    }
}
