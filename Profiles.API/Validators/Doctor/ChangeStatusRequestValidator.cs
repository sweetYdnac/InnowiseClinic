using FluentValidation;
using Shared.Models.Request.Profiles;

namespace Profiles.API.Validators.Doctor
{
    public class ChangeStatusRequestValidator : AbstractValidator<ChangeStatusRequestModel>
    {
        public ChangeStatusRequestValidator()
        {
            RuleFor(p => p.Status)
                .NotNull()
                .IsInEnum();
        }
    }
}
