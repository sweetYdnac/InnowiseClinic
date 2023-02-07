using FluentValidation;
using Shared.Models.Request.Profiles.Patient;

namespace Profiles.API.Validators.Patient
{
    public class CreatePatientRequestModelValidator : AbstractValidator<CreatePatientRequestModel>
    {
        public CreatePatientRequestModelValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty()
                .NotNull();

            RuleFor(p => p.LastName)
                .NotEmpty()
                .NotNull();

            RuleFor(p => p.DateOfBirth)
                .NotEmpty()
                .NotNull();
        }
    }
}
