namespace Profiles.Data.Interfaces.Repositories
{
    public interface IProfilesRepository
    {
        Task<int> SetInactiveStatusToPersonalAsync(Guid officeId);
        Task<int> UpdateOfficeAddressAsync(Guid officeId, string officeAddress);
    }
}
