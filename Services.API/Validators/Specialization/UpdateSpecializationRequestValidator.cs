﻿using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Services.Specialization;

namespace Services.API.Validators.Specialization
{
    public class UpdateSpecializationRequestValidator : AbstractValidator<UpdateSpecializationRequest>
    {
        public UpdateSpecializationRequestValidator()
        {
            RuleFor(r => r.Title).Required();
            RuleFor(r => r.IsActive).NotNull();
        }
    }
}
