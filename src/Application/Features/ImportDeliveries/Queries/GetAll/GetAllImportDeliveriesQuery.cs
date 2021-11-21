using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Shared.Wrapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ImportDeliveries.Queries.GetAll
{
    public class GetAllImportDeliveriesQuery : IRequest<Result<List<GetAllImportDeliveriesResponse>>>
    {
        public GetAllImportDeliveriesQuery()
        {
        }
    }

    internal class GetAllImportDeliveriesQueryHandler : IRequestHandler<GetAllImportDeliveriesQuery, Result<List<GetAllImportDeliveriesResponse>>>
    {
        private readonly IImportDeliveryRepository _repository;
        private readonly IMapper _mapper;

        public GetAllImportDeliveriesQueryHandler(IImportDeliveryRepository repository, IMapper mapper)
        {
            _repository = repository;            
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllImportDeliveriesResponse>>> Handle(GetAllImportDeliveriesQuery request, CancellationToken cancellationToken)
        {           
            var getAllImportDeliveries = await _repository.GetAllAsync();
            var mappedImportDeliveries = _mapper.Map<List<GetAllImportDeliveriesResponse>>(getAllImportDeliveries);
            return await Result<List<GetAllImportDeliveriesResponse>>.SuccessAsync(mappedImportDeliveries);
        }
    }
}