namespace Shared.Models.Request.Authorization
{
    public class PatchRolesRequestModel
    {
        public string RoleName { get; set; }
        public bool IsAddRole { get; set; }
    }
}
