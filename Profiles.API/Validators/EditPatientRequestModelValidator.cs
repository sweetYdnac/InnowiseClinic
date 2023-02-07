using FluentValidation;
using Shared.Models.Request.Profiles.Patient;

namespace Profiles.API.Validators
{
    public class EditPatientRequestModelValidator : AbstractValidator<EditPatientRequestModel>
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
