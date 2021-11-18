using Domain.Contracts;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ImportDelivery : Entity     
    {
        public ImportDelivery(DateTime importDate, long totalItens, DateTime minimalDeliveryDate, double totalDeliveryItens)
        {
            ImportDate = importDate;
            TotalItens = totalItens;
            MinimalDeliveryDate = minimalDeliveryDate;
            TotalDeliveryItens = totalDeliveryItens;
        }

        public DateTime ImportDate { get; private set; }     
        public long TotalItens { get; private set; }
        public DateTime MinimalDeliveryDate { get; private set; }
        public double TotalDeliveryItens { get; private set; }

        public ICollection<ImportDeliveryItem> ImportDeliveryItens { get; } = new List<ImportDeliveryItem>();
    }
}
