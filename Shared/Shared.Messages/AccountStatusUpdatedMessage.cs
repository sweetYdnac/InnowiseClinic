using Shared.Core.Enums;

namespace Shared.Messages
{
    public class AccountStatusUpdatedMessage
    {
        public Guid AccountId { get; set; }
        public AccountStatuses Status { get; set; }
        public string UpdaterId { get; set; }
    }
}
