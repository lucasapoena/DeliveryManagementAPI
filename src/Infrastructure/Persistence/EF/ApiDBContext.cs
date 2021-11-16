using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EF
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options)
           : base(options)
        { }

        public DbSet<ImportDelivery> ImportDeliveries { get; set; }
        public DbSet<ImportDeliveryItem> ImportDeliveryItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* 
             * ----- Classes Ignoradas -----
             * Remove a classe de Notificação e os ValueObjects para não possuirem tabelas na base de dados.
             * -----------------------------
             */
            //modelBuilder.Ignore<Password>();


            // ------ Mapeia as entidades
            #region Adiciona entidades mapeadas - Automaticamente via Assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiDBContext).Assembly);
            #endregion
            // ------ Fim - Mapeia as entidades

            base.OnModelCreating(modelBuilder);
        }

    }
}
