using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Domain.Commonts;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Core.Domain.Entities.Descripcion;
using RealStateApp.Core.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infraestructure.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }

        DbSet<Cliente> Clientes { get; set; }
        DbSet<Agente> Agentes { get; set; }
        DbSet<Administrador> Administradores { get; set; }

        DbSet<Favorita> Favoritas { get; set; }
        DbSet<Propiedad> Propiedades { get; set; }
        DbSet<Mejora> Mejoras { get; set; }
        DbSet<MejorasAplicadas> MejorasAplicadas { get; set; }

        DbSet<ImgPropiedad> ImgPropiedades { get; set; }
        DbSet<TipoPropiedad> TiposPropiedad { get; set; }
        DbSet<TipoVenta> TiposVenta { get; set; }
        



        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedby = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
