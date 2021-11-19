using Application.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.ImportDeliveryItens.Commands
{
    public class ImportDeliveryItemCommand
    {
        [ExcelColumn(1)]
        [Required]
        public DateTime DeliveryDate { get; set; }

        [ExcelColumn(2)]
        [Required]
        public string ProductName { get; set; }

        [ExcelColumn(3)]
        [Required]
        public int ProductQty { get; set; }

        [ExcelColumn(4)]
        [Required]
        public double ProductPrice { get; set; }
    }
}
