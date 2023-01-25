using FluentValidation;
using Shared.Models.Request.Authorization;

namespace Authorization.API.Validators
{
    public class PatchRolesRequestModelValidator : AbstractValidator<PatchRolesRequestModel>
    {
        public PatchRolesRequestModelValidator()
        {
            RuleFor(s => s.Id)
                .NotEmpty()
                .NotNull();

            RuleFor(s => s.RoleName)
                .NotEmpty()
                .NotNull();

            RuleFor(s => s.IsAddRole)
                .NotEmpty()
                .NotNull();
        }
    }
}
