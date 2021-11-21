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
        const int INITIAL_LINE_XLS = 2;

        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IExcelService _excelService;
        private readonly IImportDeliveryRepository _importDeliveryRepository;        

        public InsertImportDeliveryCommandHandler(
            IMapper mapper,
            IUploadService uploadService,
            IExcelService excelService,
            IImportDeliveryRepository importDeliveryRepository
        )
        {
            _mapper = mapper;
            _uploadService = uploadService;
            _excelService = excelService;
            _importDeliveryRepository = importDeliveryRepository;
        }
        public async Task<Result<Guid>> Handle(InsertImportDeliveryCommand command, CancellationToken cancellationToken)
        {
            var fileLocation = await _uploadService.UploadAsync(new Requests.UploadRequest { File = command.UploadRequest.File });
            if (!string.IsNullOrEmpty(fileLocation))
            {
                var importDeliveryItens = await _excelService.ConvertXLSToObjectAsync<ImportDeliveryItemCommand>(fileLocation);
                var validator = new ImportDeliveryItemCommandValidator();
                List<string> invalidItensErrors = new();

                var lineXLS = INITIAL_LINE_XLS;
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
                    var importDeliveryCommand = new ImportDeliveryCommand(importDeliveryItens.ToList());
                    var importDelivery = _mapper.Map<ImportDelivery>(importDeliveryCommand);

                    var model = await _importDeliveryRepository.AddAsync(importDelivery);
                    return await Result<Guid>.SuccessAsync(model.Id, "Dados importados com sucesso!");
                }
            }

            return await Result<Guid>.FailAsync("O arquivo enviado é inválido!");
        }
    }
}
