using Shared.Core.Enums;

namespace Shared.Models.Request.Authorization
{
    public class PatchAccountRequestModel
    {
        public Guid Id { get; set; }
        public AccountStatuses Status { get; set; }
    }
}
