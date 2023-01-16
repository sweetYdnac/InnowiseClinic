using Authorization.Data.Enums;
using System.Security.Claims;

namespace Authorization.Data.DataTransferObjects
{
    public class PatchAccountDTO
    {
        public AccountStatuses Status { get; set; }
        public ClaimsPrincipal UpdaterClaimsPrincipal { get; set; }
    }
}
