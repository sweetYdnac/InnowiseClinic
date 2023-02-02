using Offices.Application.DTOs;
using Offices.Domain.Entities;

namespace Offices.Application.Interfaces.Repositories
{
    public interface IOfficeRepository
    {
        Task<(IEnumerable<OfficeEntity> offices, int totalCount)> GetPagedOfficesAsync(GetPagedOfficesDTO dto);
        Task<OfficeEntity> GetByIdAsync(Guid id);
    }
}
