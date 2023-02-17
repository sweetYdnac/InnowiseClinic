using FluentValidation;
using Shared.Models.Request.Authorization;

namespace Authorization.API.Validators
{
    public class PatchRolesRequestValidator : AbstractValidator<PatchRolesRequest>
    {
        public PatchRolesRequestValidator()
        {
            RuleFor(s => s.RoleName)
                .NotEmpty()
                .NotNull();

            RuleFor(s => s.IsAddRole)
                .NotEmpty()
                .NotNull();
        }
    }
}
