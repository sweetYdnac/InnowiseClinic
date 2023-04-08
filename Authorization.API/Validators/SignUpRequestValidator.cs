using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Authorization;

namespace Authorization.API.Validators
{
    public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
    {
        public SignUpRequestValidator()
        {
            RuleFor(s => s.Email)
                .Required()
                .EmailAddress();

            RuleFor(s => s.Password)
                .Length(6, 15);
        }
    }
}
