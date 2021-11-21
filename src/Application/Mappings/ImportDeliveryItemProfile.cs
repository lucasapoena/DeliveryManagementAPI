using Application.Features.ImportDeliveryItens.Commands;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ImportDeliveryItemProfile : Profile
    {
        public ImportDeliveryItemProfile()
        {
            CreateMap<ImportDeliveryItemCommand, ImportDeliveryItem>().ReverseMap();
        }
    }
}