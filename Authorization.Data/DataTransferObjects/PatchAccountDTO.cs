using Shared.Core.Enums;

namespace Authorization.Data.DataTransferObjects
{
    public class PatchAccountDTO
    {
        public AccountStatuses Status { get; set; }
        public string UpdaterId { get; set; }
    }
}
