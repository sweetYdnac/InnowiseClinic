using FluentValidation;
using Shared.Models.Request.Authorization;

namespace Authorization.API.Validators
{
    public class PatchAccountRequestModelValidator : AbstractValidator<PatchAccountRequestModel>
    {
        public PatchAccountRequestModelValidator()
        {
            RuleFor(s => s.Id)
                .NotEmpty()
                .NotNull();

            RuleFor(s => s.Status)
                .IsInEnum();
        }
    }
}
