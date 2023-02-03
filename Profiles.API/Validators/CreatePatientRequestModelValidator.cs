using FluentValidation;
using Shared.Models.Request.Profiles.Patient;

namespace Profiles.API.Validators
{
    public class CreatePatientRequestModelValidator : AbstractValidator<CreatePatientRequestModel>
    {
        public CreatePatientRequestModelValidator()
        {
            RuleFor(p => p.Firstname)
                .NotEmpty()
                .NotNull();

            RuleFor(p => p.Lastname)
                .NotEmpty()
                .NotNull();

            RuleFor(p => p.AccountId)
                .NotEmpty()
                .NotNull();

            RuleFor(p => p.DateOfBirth)
                .NotEmpty()
                .NotNull();
        }
    }
}
