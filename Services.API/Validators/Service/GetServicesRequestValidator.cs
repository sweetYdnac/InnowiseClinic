using FluentValidation;
using Shared.Models.Request.Services.Service;

namespace Services.API.Validators.Service
{
    public class GetServicesRequestValidator : AbstractValidator<GetServicesRequest>
    {
        public GetServicesRequestValidator()
        {
            RuleFor(p => p.CurrentPage)
                .NotNull()
                .GreaterThan(0);

            RuleFor(p => p.PageSize)
                .NotNull()
                .InclusiveBetween(1, 50);

            RuleFor(r => r.IsActive).NotNull();
        }
    }
}
