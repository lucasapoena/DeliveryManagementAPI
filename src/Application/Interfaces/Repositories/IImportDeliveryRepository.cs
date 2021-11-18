using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IImportDeliveryRepository
    {
        Task<ImportDelivery> AddAsync(ImportDelivery entity);
        Task<ImportDelivery> GetByIdAsync(Guid id);
        Task<List<ImportDelivery>> GetAllAsync();
    }
}
