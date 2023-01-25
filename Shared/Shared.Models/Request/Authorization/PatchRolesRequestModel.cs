namespace Shared.Models.Request.Authorization
{
    public class PatchRolesRequestModel
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public bool IsAddRole { get; set; }
    }
}
