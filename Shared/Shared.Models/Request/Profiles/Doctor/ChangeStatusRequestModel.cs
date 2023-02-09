using Shared.Core.Enums;

namespace Shared.Models.Request.Profiles.Doctor
{
    public class ChangeStatusRequestModel
    {
        public AccountStatuses Status { get; set; }
    }
}
