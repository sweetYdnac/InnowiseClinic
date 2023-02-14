namespace Profiles.Business.Interfaces.Services
{
    public interface IProfilesService
    {
        Task SetInactiveStatusToPersonalAsync(Guid officeId);
        Task UpdateOfficeAddressAsync(Guid officeId, string officeAddress);
    }
}
