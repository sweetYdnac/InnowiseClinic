using Shared.Models.Response.Services.Service;

namespace Services.Business.Interfaces
{
    public interface IServicesService
    {
        Task<ServiceResponse> GetByIdAsync(Guid id);
    }
}
