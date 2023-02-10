using FluentValidation;
using Profiles.API.Extensions;
using Shared.Models.Request.Profiles.Patient;

namespace Profiles.API.Validators.Patient
{
    public class EditPatientRequestValidator : AbstractValidator<UpdatePatientRequestModel>
    {
        public EditPatientRequestValidator()
        {
            RuleFor(p => p.FirstName).Required();
            RuleFor(p => p.LastName).Required();
            RuleFor(p => p.DateOfBirth).Required();
        }
    }
}
