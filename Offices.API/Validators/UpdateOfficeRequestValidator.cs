﻿using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Offices;

namespace Offices.API.Validators
{
    public class UpdateOfficeRequestValidator : AbstractValidator<UpdateOfficeRequest>
    {
        public UpdateOfficeRequestValidator()
        {
            RuleFor(m => m.City).Required();
            RuleFor(m => m.Street).Required();
            RuleFor(m => m.HouseNumber).Required();
            RuleFor(m => m.OfficeNumber).Required();
            RuleFor(m => m.RegistryPhoneNumber).Required();
            RuleFor(m => m.IsActive).NotNull();
        }
    }
}
