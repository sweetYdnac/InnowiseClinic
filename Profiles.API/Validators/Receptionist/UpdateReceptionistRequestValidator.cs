using FluentValidation;
using Profiles.API.Extensions;
using Shared.Models.Request.Profiles.Receptionist;

namespace Profiles.API.Validators.Receptionist
{
    public class UpdateReceptionistRequestValidator : AbstractValidator<UpdateReceptionistRequestModel>
    {
        public UpdateReceptionistRequestValidator()
        {
            RuleFor(p => p.FirstName).Required();
            RuleFor(p => p.LastName).Required();
            RuleFor(p => p.OfficeId).Required();
            RuleFor(p => p.OfficeAddress).Required();
            RuleFor(p => p.Status)
                .Required()
                .IsInEnum();
        }
    }
}
