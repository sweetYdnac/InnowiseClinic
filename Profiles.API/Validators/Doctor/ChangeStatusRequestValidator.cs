using FluentValidation;
using Shared.Models.Request.Profiles.Doctor;

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
