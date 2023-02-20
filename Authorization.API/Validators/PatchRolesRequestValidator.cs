using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Authorization;

namespace Authorization.API.Validators
{
    public class PatchRolesRequestValidator : AbstractValidator<PatchRolesRequest>
    {
        public PatchRolesRequestValidator()
        {
            RuleFor(s => s.RoleName).Required();
            RuleFor(s => s.IsAddRole).Required();
        }
    }
}
