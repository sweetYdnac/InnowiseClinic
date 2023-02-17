using FluentValidation;
using Profiles.API.Extensions;
using Shared.Models.Request.Profiles.Patient;

namespace Profiles.API.Validators.Patient
{
    public class UpdatePatientRequestValidator : AbstractValidator<UpdatePatientRequest>
    {
        public UpdatePatientRequestValidator()
        {
            RuleFor(p => p.FirstName).Required();
            RuleFor(p => p.LastName).Required();
            RuleFor(p => p.DateOfBirth).Required();
            RuleFor(p => p.PhoneNumber).Required();
        }
    }
}
