using System;

namespace Application.Features.ImportDeliveries.Queries.GetAll
{
    public class GetAllImportDeliveriesResponse
    {
        public Guid Id { get; set; }
        public DateTime ImportDate { get; set; }
        public long TotalItens { get; set; }
        public DateTime MinimalDeliveryDate { get; set; }
        public double TotalDeliveryItens { get; set; }
    }
}