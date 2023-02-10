﻿using FluentValidation;
using Profiles.API.Extensions;
using Shared.Models.Request.Profiles.Patient;

namespace Profiles.API.Validators.Patient
{
    public class CreatePatientRequestValidator : AbstractValidator<CreatePatientRequestModel>
    {
        public CreatePatientRequestValidator()
        {
            RuleFor(p => p.FirstName).Required();
            RuleFor(p => p.LastName).Required();
            RuleFor(p => p.DateOfBirth).Required();
        }
    }
}
