using FluentValidation;
using Shared.Models.Request.Offices;

namespace Offices.API.Validators
{
    public class GetOfficesRequestValidator : AbstractValidator<GetOfficesRequest>
    {
        public GetOfficesRequestValidator()
        {
            RuleFor(r => r.CurrentPage)
                .NotNull()
                .GreaterThan(0);

            RuleFor(r => r.PageSize)
                .NotNull()
                .InclusiveBetween(1, 50);
        }
    }
}
