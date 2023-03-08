using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Appointments.Appointment;

namespace Appointments.Write.API.Validators.Appointment
{
    public class CreateAppointmentRequestValidator : AbstractValidator<CreateAppointmentRequest>
    {
        public CreateAppointmentRequestValidator()
        {
            RuleFor(r => r.PatientId).Required();
            RuleFor(r => r.DoctorId).Required();
            RuleFor(r => r.ServiceId).Required();
            RuleFor(r => r.OfficeId).Required();
            RuleFor(r => r.Date)
                .Required()
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now));

            RuleFor(r => r.Time)
                .Required()
                .InclusiveBetween(
                new TimeOnly(5, 00),
                new TimeOnly(22, 00))
                .Must(t => t.Minute % 10 == 0);

            RuleFor(r => r.Duration)
                .Required()
                .GreaterThan(0);

            RuleFor(r => r.PatientFullName).Required();
            RuleFor(r => r.PatientPhoneNumber).Required();
            RuleFor(r => r.DoctorFullName).Required();
            RuleFor(r => r.ServiceName).Required();

        }
    }
}
