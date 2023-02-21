using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Profiles.Patient;

namespace Profiles.API.Validators.Patient
{
    public class CreatePatientRequestValidator : AbstractValidator<CreatePatientRequest>
    {
        public CreatePatientRequestValidator()
        {
            RuleFor(p => p.FirstName).Required();
            RuleFor(p => p.LastName).Required();
            RuleFor(p => p.DateOfBirth).Required();
            RuleFor(p => p.PhoneNumber).Required();
        }
    }
}
