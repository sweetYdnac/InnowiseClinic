using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Appointments.Appointment;

namespace Appointments.Write.API.Validators.Appointment
{
    public class RescheduleAppointmentRequestValidator : AbstractValidator<RescheduleAppointmentRequest>
    {
        public RescheduleAppointmentRequestValidator()
        {
            RuleFor(r => r.DoctorId).Required();
            RuleFor(r => r.DoctorFullName).Required();

            RuleFor(r => r.Date)
                .Required()
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now));

            RuleFor(r => r.Time)
                .Required()
                .Must(t => t.Minute % 10 == 0)
                .WithMessage("Time slot duration should be divided by 10.");

        }
    }
}
