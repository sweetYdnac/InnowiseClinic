using FluentValidation;
using Shared.Models.Request.Profiles.Patient;

namespace Profiles.API.Validators.Patient
{
    public class LinkToAccountRequestModelValidator : AbstractValidator<LinkToAccountRequestModel>
    {
        public LinkToAccountRequestModelValidator()
        {
            RuleFor(p => p.AccountId)
                .NotNull()
                .NotEmpty();
        }
    }
}
