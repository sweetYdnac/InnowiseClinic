using Profiles.Business.Interfaces.Repositories;
using Profiles.Business.Interfaces.Services;
using Shared.Exceptions;
using Shared.Models.Response.Profiles.Receptionist;

namespace Profiles.Business.Implementations.Services
{
    public class ReceptionistsService : IReceptionistsService
    {
        private readonly IReceptionistsRepository _receptionistsRepository;

        public ReceptionistsService(IReceptionistsRepository receptionistsRepository) => _receptionistsRepository = receptionistsRepository;

        public async Task<ReceptionistResponse> GetByIdAsync(Guid id)
        {
            var response = await _receptionistsRepository.GetByIdAsync(id);

            return response is null
                    ? throw new NotFoundException($"Receptionist's profile with id = {id} doesn't exist.")
                    : response;
        }
    }
}
