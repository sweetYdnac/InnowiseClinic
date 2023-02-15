using Shared.Core.Enums;

namespace Profiles.Data.DTOs
{
    public class ChangeStatusDTO
    {
        public AccountStatuses Status { get; set; }
        public string UpdaterId { get; set; }
    }
}
