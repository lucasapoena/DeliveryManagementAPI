using Domain.Contracts;
using System;

namespace Domain.Entities
{
    public class ImportDeliveryItem : Entity      
    {
        public ImportDeliveryItem()
        {

        }
        public ImportDeliveryItem(DateTime deliveryDate, string productName, int productQty, int productPrice, ImportDelivery importDelivery)
        {
            DeliveryDate = deliveryDate;
            ProductName = productName;
            ProductQty = productQty;
            ProductPrice = productPrice;
            ImportDelivery = importDelivery;
            ImportDeliveryId = importDelivery.Id;
        }

        public DateTime DeliveryDate { get; private set; }
        public string ProductName { get; private set; }
        public int ProductQty { get; private set; }
        public int ProductPrice { get; private set; }

        public ImportDelivery ImportDelivery { get; private set; }
        public Guid ImportDeliveryId { get; private set; }       
    }
}
