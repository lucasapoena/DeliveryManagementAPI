using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EF.Map
{
    public class ImportDeliveryMap : IEntityTypeConfiguration<ImportDelivery>
    {
        public void Configure(EntityTypeBuilder<ImportDelivery> builder)
        {
            #region "Nome da Tabela"
            builder.ToTable("ImportDeliveries");
            #endregion

            #region "Primary Key"
            builder.HasKey(x => x.Id);
            #endregion

            #region "Mapeamento das Propriedades da Entity"
            builder.Property(x => x.ImportDate)                
                .IsRequired();

            builder.Property(x => x.TotalItens)
                .IsRequired();

            builder.Property(x => x.MinimalDeliveryDate)
                .IsRequired();

            builder.Property(x => x.TotalDeliveryItens)
                .IsRequired();
            #endregion

            #region "Mapeamento dos ValueObject"
            // ---- N/A -----
            #endregion

            #region "Mapeamento dos Relacionamentos com outras entidades"
            // ---- N/A -----
            #endregion
        }
    }
}
