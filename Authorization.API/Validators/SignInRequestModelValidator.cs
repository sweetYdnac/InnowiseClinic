﻿using FluentValidation;
using Shared.Models.Request.Authorization;

namespace Authorization.API.Validators
{
    public class SignInRequestModelValidator : AbstractValidator<SignInRequestModel>
    {
        public SignInRequestModelValidator()
        {
            RuleFor(s => s.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();

            RuleFor(s => s.Password)
                .NotEmpty()
                .NotNull()
                .Length(6, 15);
        }
    }
}
