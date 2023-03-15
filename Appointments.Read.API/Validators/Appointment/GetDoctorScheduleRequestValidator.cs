using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Appointments.Appointment;

namespace Appointments.Read.API.Validators.Appointment
{
    public class GetDoctorScheduleRequestValidator : AbstractValidator<GetDoctorScheduleRequest>
    {
        public GetDoctorScheduleRequestValidator()
        {
            RuleFor(p => p.CurrentPage)
                .NotNull()
                .GreaterThan(0);

            RuleFor(p => p.PageSize)
                .NotNull()
                .InclusiveBetween(1, 50);

            RuleFor(p => p.Date).Required();
        }
    }
}
