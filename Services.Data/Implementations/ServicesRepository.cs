using Microsoft.EntityFrameworkCore;
using Services.Data.Contexts;
using Services.Data.Entities;
using Services.Data.Interfaces;

namespace Services.Data.Implementations
{
    public class ServicesRepository : Repository<Service>, IServicesRepository
    {
        public ServicesRepository(ServicesDbContext database) 
            : base(database) { }

        public async Task DisableAsync(Guid specializationId)
        {
            await DbSet
                .Where(s => s.SpecializationId.Equals(specializationId))
                .ExecuteUpdateAsync(e => e.SetProperty(s => s.IsActive, s => false));
        }
    }
}
