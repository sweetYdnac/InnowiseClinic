﻿using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Profiles.Receptionist;

namespace Profiles.API.Validators.Receptionist
{
    public class CreateReceptionistRequestValidator : AbstractValidator<CreateReceptionistRequest>
    {
        public CreateReceptionistRequestValidator()
        {
            RuleFor(p => p.Id).Required();
            RuleFor(p => p.FirstName).Required();
            RuleFor(p => p.LastName).Required();
            RuleFor(p => p.OfficeId).Required();
            RuleFor(p => p.OfficeAddress).Required();
            RuleFor(p => p.Status)
                .Required()
                .IsInEnum();

            RuleFor(s => s.Email)
                .Required()
                .EmailAddress();

            RuleFor(s => s.Password).Length(6, 15);
        }
    }
}
