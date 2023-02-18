using Shared.Core.Enums;

namespace Profiles.Business.Interfaces.Services
{
    public interface IMessageService
    {
        Task SendAccountStatusUpdatedMessageAsync(Guid accountId, AccountStatuses status, string updaterId);
        Task SendProfileDeletedMessageAsync(Guid photoId);
    }
}
