﻿using FluentValidation;
using Shared.Models.Request.Profiles.Doctor;

namespace Profiles.API.Validators.Doctor
{
    public class GetDoctorsRequestModelValidator : AbstractValidator<GetDoctorsRequestModel>
    {
        public GetDoctorsRequestModelValidator()
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
