using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ImportDeliveryRepository : IImportDeliveryRepository
    {
        private readonly ApiDBContext _dbContext;

        public ImportDeliveryRepository(ApiDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ImportDelivery> AddAsync(ImportDelivery entity)
        {
            await _dbContext.Set<ImportDelivery>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<ImportDelivery>> GetAllAsync()
        {    
            return await _dbContext
                .Set<ImportDelivery>()
                .OrderBy(x => x.ImportDate)
                .ToListAsync();
        }

        public async Task<ImportDelivery> GetByIdAsync(Guid id)
        {
            return await _dbContext
                .Set<ImportDelivery>()
                .Include(x => x.ImportDeliveryItens)
                .FirstOrDefaultAsync(x => x.Id == id);              
        }
    }
}
