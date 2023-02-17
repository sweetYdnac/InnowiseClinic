using FluentValidation;
using Shared.Models.Request.Offices;

namespace Offices.API.Validators
{
    public class GetOfficesRequestModelValidator : AbstractValidator<GetOfficesRequest>
    {
        public GetOfficesRequestModelValidator()
        {
            RuleFor(r => r.PageNumber)
                .NotNull()
                .GreaterThan(0);

            RuleFor(r => r.PageSize)
                .NotNull()
                .InclusiveBetween(1, 50);
        }
    }
}
