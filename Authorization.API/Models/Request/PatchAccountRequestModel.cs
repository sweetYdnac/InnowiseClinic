using Authorization.Data.Enums;

namespace Authorization.API.Models.Request
{
    public class PatchAccountRequestModel
    {
        public Guid Id { get; set; }
        public AccountStatuses Status { get; set; }
    }
}
