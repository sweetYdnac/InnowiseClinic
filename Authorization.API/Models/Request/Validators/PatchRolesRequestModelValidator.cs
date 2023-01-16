using FluentValidation;

namespace Authorization.API.Models.Request.Validators
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
