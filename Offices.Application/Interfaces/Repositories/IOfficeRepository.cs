using Offices.Application.Features.Office.Queries;
using Offices.Domain.Entities;

namespace Offices.Application.Interfaces.Repositories
{
    public interface IOfficeRepository
    {
        Task<(IEnumerable<OfficeEntity> offices, int totalCount)> GetPagedOffices(GetOfficesQuery request);
    }
}
