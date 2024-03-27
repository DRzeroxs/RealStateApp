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

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Agente> Agentes { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Favorita> Favoritas { get; set; }
        public DbSet<Propiedad> Propiedades { get; set; }
        public DbSet<Mejora> Mejoras { get; set; }
        public DbSet<MejorasAplicadas> MejorasAplicadas { get; set; }
        public DbSet<ImgPropiedad> ImgPropiedades { get; set; }
        public DbSet<TipoPropiedad> TiposPropiedad { get; set; }
        public DbSet<TipoVenta> TiposVenta { get; set; }
        

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar el comportamiento predeterminado para los índices
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var index in entityType.GetIndexes())
                {
                    index.IsUnique = false; // Establecer todos los índices como no únicos
                }
            }
        }
    }


}
