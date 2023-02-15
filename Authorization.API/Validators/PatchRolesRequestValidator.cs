using FluentValidation;
using Shared.Models.Request.Authorization;

namespace Authorization.API.Validators
{
    public class PatchRolesRequestValidator : AbstractValidator<PatchRolesRequestModel>
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
