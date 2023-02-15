using Shared.Core.Enums;

namespace Shared.Models.Messages
{
    public class AccountStatusUpdated
    {
        public Guid AccountId { get; set; }
        public AccountStatuses Status { get; set; }
        public string UpdaterId { get; set; }
    }
}
