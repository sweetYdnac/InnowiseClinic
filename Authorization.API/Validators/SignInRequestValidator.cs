using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Authorization;

namespace Authorization.API.Validators
{
    public class SignInRequestValidator : AbstractValidator<SignInRequest>
    {
        public SignInRequestValidator()
        {
            RuleFor(s => s.Email)
                .Required()
                .EmailAddress();

            RuleFor(s => s.Password)
                .Required()
                .Length(6, 15);
        }
    }
}
