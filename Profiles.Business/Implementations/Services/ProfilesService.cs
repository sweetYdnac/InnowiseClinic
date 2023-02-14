using Profiles.Business.Interfaces.Repositories;
using Profiles.Business.Interfaces.Services;
using Serilog;

namespace Profiles.Business.Implementations.Services
{
    public class ProfilesService : IProfilesService
    {
        private readonly IProfilesRepository _profilesRepository;

        public ProfilesService(IProfilesRepository profilesRepository) => _profilesRepository = profilesRepository;

        public async Task SetInactiveStatusToPersonalAsync(Guid officeId)
        {
            var result = await _profilesRepository.SetInactiveStatusToPersonalAsync(officeId);

            if (result == 0)
            {
                Log.Information("There are no personal with {@officeId}.", officeId);
            }
        }

        public async Task UpdateOfficeAddressAsync(Guid officeId, string officeAddress)
        {
            var result = await _profilesRepository.UpdateOfficeAddressAsync(officeId, officeAddress);

            if (result == 0)
            {
                Log.Information("There are no personal with {@officeId}.", officeId);
            }
        }
    }
}
