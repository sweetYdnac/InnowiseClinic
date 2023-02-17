using FluentValidation;
using Shared.Models.Request.Profiles.Patient;

namespace Profiles.API.Validators.Patient
{
    public class GetPatientsRequestValidator : AbstractValidator<GetPatientsRequest>
    {
        public GetPatientsRequestValidator()
        {
            RuleFor(p => p.PageNumber)
                .NotNull()
                .GreaterThan(0);

            RuleFor(p => p.PageSize)
                .NotNull()
                .InclusiveBetween(1, 50);
        }
    }
}
