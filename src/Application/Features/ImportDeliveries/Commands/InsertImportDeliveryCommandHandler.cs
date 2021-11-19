using Application.Features.ImportDeliveryItens.Commands;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Validators.Features.ImportDeliveryItens.Commands;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ImportDeliveries.Commands
{
    public class InsertImportDeliveryCommandHandler : IRequestHandler<InsertImportDeliveryCommand, Result<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IExcelService _excelService;
        private readonly IImportDeliveryRepository _importDeliveryRepository;
        private readonly IImportDeliveryItemRepository _importDeliveryItemRepository;

        public InsertImportDeliveryCommandHandler(
            IMapper mapper,
            IUploadService uploadService,
            IExcelService excelService,
            IImportDeliveryRepository importDeliveryRepository,
            IImportDeliveryItemRepository importDeliveryItemRepository
        )
        {
            _mapper = mapper;
            _uploadService = uploadService;
            _excelService = excelService;
            _importDeliveryRepository = importDeliveryRepository;
            _importDeliveryItemRepository = importDeliveryItemRepository;
        }
        public async Task<Result<Guid>> Handle(InsertImportDeliveryCommand command, CancellationToken cancellationToken)
        {
            var fileLocation = await _uploadService.UploadAsync(new Requests.UploadRequest { File = command.UploadRequest.File });
            if (!string.IsNullOrEmpty(fileLocation))
            {
                var importDeliveryItens = await _excelService.ConvertXLSToObject<ImportDeliveryItemCommand>(fileLocation);
                var validator = new ImportDeliveryItemCommandValidator();
                List<string> invalidItensErrors = new();

                var lineXLS = 2;
                foreach (var importDeliveryItem in importDeliveryItens)
                {
                    var result = validator.Validate(importDeliveryItem);
                    if (!result.IsValid)
                    {
                        foreach (var messageError in result.Errors)
                        {
                            var message = $"Error :: Linha - {lineXLS} :: {messageError}";
                            invalidItensErrors.Add(message);
                        }
                    }

                    lineXLS++;
                }

                if (invalidItensErrors.Any())
                {
                    return await Result<Guid>.FailAsync(invalidItensErrors);
                }
                else
                {
                    var importDate = DateTime.UtcNow;
                    var totalItens = importDeliveryItens.Sum(x => x.ProductQty);
                    var minimalDeliveryDate = importDeliveryItens.Min(x => x.DeliveryDate);
                    var totalDeliveryItens = importDeliveryItens.Sum(x => x.ProductPrice * x.ProductQty);

                    var importDelivery = new ImportDelivery(importDate, totalItens, minimalDeliveryDate, totalDeliveryItens);

                    var model = await _importDeliveryRepository.AddAsync(importDelivery);
                    return await Result<Guid>.SuccessAsync(model.Id, "Dados importados com sucesso!");
                }
            }

            return await Result<Guid>.FailAsync("O arquivo enviado é inválido!");
        }
    }
}
