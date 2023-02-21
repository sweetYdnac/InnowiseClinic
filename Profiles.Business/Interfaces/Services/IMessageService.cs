using Shared.Core.Enums;

namespace Profiles.Business.Interfaces.Services
{
    public interface IMessageService
    {
        Task SendUpdateAccountStatusMessageAsync(Guid accountId, AccountStatuses status, string updaterId);
        Task SendDeletePhotoMessageAsync(Guid photoId);
    }
}
