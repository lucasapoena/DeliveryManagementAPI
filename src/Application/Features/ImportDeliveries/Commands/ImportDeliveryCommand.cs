using Application.Features.ImportDeliveryItens.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Features.ImportDeliveries.Commands
{
    public class ImportDeliveryCommand
    {
        public DateTime ImportDate { get; set; }
        public long TotalItens { get; set; }
        public DateTime MinimalDeliveryDate { get; set; }
        public double TotalDeliveryItens { get; set; }

        public List<ImportDeliveryItemCommand> ImportDeliveryItens { get; set; } = new();

        public ImportDeliveryCommand()
        {

        }

        public ImportDeliveryCommand(List<ImportDeliveryItemCommand> importDeliveryItens)
        {
            ImportDeliveryItens = importDeliveryItens;
            ImportDate = DateTime.UtcNow;
            TotalItens = importDeliveryItens.Sum(x => x.ProductQty);
            MinimalDeliveryDate = importDeliveryItens.Min(x => x.DeliveryDate);
            TotalDeliveryItens = Math.Round(importDeliveryItens.Sum(x => x.ProductPrice * x.ProductQty), 2);            
        }
    }
}
