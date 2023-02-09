using FluentValidation;
using Profiles.API.Extensions;
using Shared.Models.Request.Profiles.Doctor;

namespace Profiles.API.Validators.Doctor
{
    public class UpdateDoctorRequestModelValidator : AbstractValidator<UpdateDoctorRequestModel>
    {
        public UpdateDoctorRequestModelValidator()
        {
            RuleFor(p => p.FirstName).Required();
            RuleFor(p => p.LastName).Required();
            RuleFor(p => p.DateOfBirth).Required();
            RuleFor(p => p.CareerStartYear).Required();
            RuleFor(p => p.SpecializationId).Required();
            RuleFor(p => p.SpecializationName).Required();
            RuleFor(p => p.OfficeId).Required();
            RuleFor(p => p.OfficeAddress).Required();
        }
    }
}
