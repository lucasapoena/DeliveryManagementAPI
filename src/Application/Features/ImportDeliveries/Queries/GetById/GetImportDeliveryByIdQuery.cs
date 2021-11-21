using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using MediatR;
using Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ImportDeliveries.Queries.GetById
{
    public class GetImportDeliveryByIdQuery : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
    }

    internal class GetImportDeliveryByIdQueryHandler : IRequestHandler<GetImportDeliveryByIdQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IImportDeliveryRepository _repository; 

        public GetImportDeliveryByIdQueryHandler(IExcelService excelService, IImportDeliveryRepository repository)
        {
            _excelService = excelService;
            _repository = repository;
        }

        public async Task<Result<string>> Handle(GetImportDeliveryByIdQuery query, CancellationToken cancellationToken)
        {
            var importDelivery = await _repository.GetByIdAsync(query.Id);
            if (importDelivery == null)
            {
                return await Result<string>.FailAsync("There is no registered import with registered ID!");
            }
            else
            {
                var dataItens = importDelivery.ImportDeliveryItens;
                var data = await _excelService.ExportAsync(
                    dataItens, 
                    mappers: new Dictionary<string, Func<ImportDeliveryItem, object>>
                    {
                        { "DeliveryDate", item => item.DeliveryDate.ToString("dd/MM/yyyy") },
                        { "ProductName", item => item.ProductName },
                        { "ProductQty", item => item.ProductQty },
                        { "ProductPrice", item => item.ProductPrice},
                        { "TotalPrice", item => item.TotalPrice}
                    }, sheetName: "ImportDeliveryItens");

                return await Result<string>.SuccessAsync(data: data);
            }
        }
    }
}