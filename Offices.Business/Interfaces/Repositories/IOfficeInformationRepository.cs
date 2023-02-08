using Offices.Data.DTOs;
using Shared.Models.Response.Offices;

namespace Offices.Business.Interfaces.Repositories
{
    public interface IOfficeInformationRepository
    {
        Task<(IEnumerable<OfficeInformationResponse> offices, int totalCount)> GetPagedOfficesAsync(GetPagedOfficesDTO dto);
    }
}
