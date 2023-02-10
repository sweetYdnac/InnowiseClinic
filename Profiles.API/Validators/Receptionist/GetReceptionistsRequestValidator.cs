using FluentValidation;
using Shared.Models.Request.Profiles.Receptionist;

namespace Profiles.API.Validators.Receptionist
{
    public class GetReceptionistsRequestValidator : AbstractValidator<GetReceptionistsRequestModel>
    {
        public GetReceptionistsRequestValidator()
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
