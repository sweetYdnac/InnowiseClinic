using FluentValidation;
using Shared.Models.Request.Profiles.Patient;

namespace Profiles.API.Validators.Patient
{
    public class EditPatientRequestModelValidator : AbstractValidator<UpdatePatientRequestModel>
    {
        public EditPatientRequestModelValidator()
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
