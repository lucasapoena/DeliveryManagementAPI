using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EF.Map
{
    public class ImportDeliveryItemMap : IEntityTypeConfiguration<ImportDeliveryItem>
    {
        public void Configure(EntityTypeBuilder<ImportDeliveryItem> builder)
        {
            #region "Nome da Tabela"
            builder.ToTable("ImportDeliveryItens");
            #endregion

            #region "Primary Key"
            builder.HasKey(x => x.Id);
            #endregion

            #region "Mapeamento das Propriedades da Entity"
            builder.Property(x => x.DeliveryDate)                
                .IsRequired();

            builder.Property(x => x.ProductName)
                .IsRequired();

            builder.Property(x => x.ProductQty)
                .IsRequired();

            builder.Property(x => x.ProductPrice)
                .IsRequired();
            #endregion

            #region "Mapeamento dos ValueObject"
            // ---- N/A -----
            #endregion

            #region "Mapeamento dos Relacionamentos com outras entidades"
            builder
                .HasOne(importDeliveryItem => importDeliveryItem.ImportDelivery)
                .WithMany(importDelivery => importDelivery.ImportDeliveryItens);
            #endregion
        }
    }
}
