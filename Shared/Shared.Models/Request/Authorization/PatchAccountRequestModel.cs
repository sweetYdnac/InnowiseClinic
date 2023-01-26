using Shared.Core.Enums;

namespace Shared.Models.Request.Authorization
{
    public class PatchAccountRequestModel
    {
        public AccountStatuses Status { get; set; }
    }
}
