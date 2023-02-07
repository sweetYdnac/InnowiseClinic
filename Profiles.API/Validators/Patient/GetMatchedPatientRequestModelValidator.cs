using FluentValidation;
using Shared.Models.Request.Profiles.Patient;

namespace Profiles.API.Validators.Patient
{
    public class GetMatchedPatientRequestModelValidator : AbstractValidator<GetMatchedPatientRequestModel>
    {
        public GetMatchedPatientRequestModelValidator()
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
