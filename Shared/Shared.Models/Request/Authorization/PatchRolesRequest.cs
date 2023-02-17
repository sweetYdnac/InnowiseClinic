namespace Shared.Models.Request.Authorization
{
    public class PatchRolesRequest
    {
        public string RoleName { get; set; }
        public bool IsAddRole { get; set; }
    }
}
