using FluentValidation;
using Shared.Models.Request.Profiles.Doctor;

namespace Profiles.API.Validators.Doctor
{
    public class GetDoctorsRequestValidator : AbstractValidator<GetDoctorsRequest>
    {
        public GetDoctorsRequestValidator()
        {
            RuleFor(p => p.PageNumber)
                .NotNull()
                .GreaterThan(0);

            RuleFor(p => p.PageSize)
                .NotNull()
                .InclusiveBetween(1, 50);

            RuleFor(p => p.OnlyAtWork)
                .NotNull();
        }
    }
}
