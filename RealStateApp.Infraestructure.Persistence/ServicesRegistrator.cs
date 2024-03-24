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
            services.AddTransient(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            #endregion
        }
    }
}
