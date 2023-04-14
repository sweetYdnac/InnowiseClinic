using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Services.Specialization;

namespace Services.API.Validators.Specialization
{
    public class GetSpecializationsRequestValidator : AbstractValidator<GetSpecializationsRequest>
    {
        public GetSpecializationsRequestValidator()
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
