using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Profiles.Patient;

namespace Shared.Models.Validators.Patient
{
    public class GetMatchedPatientRequestValidator : AbstractValidator<GetMatchedPatientRequest>
    {
        public GetMatchedPatientRequestValidator()
        {
            RuleFor(p => p.FirstName).Required();
            RuleFor(p => p.LastName).Required();
            RuleFor(p => p.DateOfBirth).Required();
        }
    }
}
