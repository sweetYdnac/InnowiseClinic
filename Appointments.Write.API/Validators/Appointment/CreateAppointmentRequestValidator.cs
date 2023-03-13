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
                .Must(t => t.Minute % 10 == 0)
                .WithMessage("Time slot duration should be divided by 10.");

            RuleFor(r => r.Duration)
                .Required()
                .GreaterThan(0)
                .Must(p => p % 10 == 0)
                .WithMessage("Time slot duration should be divided by 10.");

            RuleFor(r => r.PatientFullName).Required();
            RuleFor(r => r.PatientPhoneNumber).Required();
            RuleFor(r => r.PatientDateOfBirth).Required();
            RuleFor(r => r.DoctorFullName).Required();
            RuleFor(r => r.DoctorSpecializationName).Required();
            RuleFor(r => r.ServiceName).Required();
        }
    }
}
