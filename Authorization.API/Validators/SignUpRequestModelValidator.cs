using FluentValidation;
using Shared.Models.Request.Authorization;

namespace Authorization.API.Validators
{
    public class SignUpRequestModelValidator : AbstractValidator<SignUpRequestModel>
    {
        public SignUpRequestModelValidator()
        {
            RuleFor(s => s.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();

            RuleFor(s => s.Password)
                .NotEmpty()
                .NotNull()
                .Length(6, 15);

            RuleFor(s => s.PasswordConfirmation)
                .NotEmpty()
                .NotNull()
                .Length(6, 15)
                .Equal(model => model.Password);
        }
    }
}
