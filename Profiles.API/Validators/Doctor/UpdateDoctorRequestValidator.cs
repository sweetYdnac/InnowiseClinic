using FluentValidation;
using Profiles.API.Extensions;
using Shared.Models.Request.Profiles.Doctor;

namespace Profiles.API.Validators.Doctor
{
    public class UpdateDoctorRequestValidator : AbstractValidator<UpdateDoctorRequestModel>
    {
        public UpdateDoctorRequestValidator()
        {
            RuleFor(p => p.FirstName).Required();
            RuleFor(p => p.LastName).Required();
            RuleFor(p => p.DateOfBirth).Required();
            RuleFor(p => p.CareerStartYear).Required();
            RuleFor(p => p.SpecializationId).Required();
            RuleFor(p => p.SpecializationName).Required();
            RuleFor(p => p.OfficeId).Required();
            RuleFor(p => p.OfficeAddress).Required();
            RuleFor(p => p.Status)
                .Required()
                .IsInEnum();

        }
    }
}
