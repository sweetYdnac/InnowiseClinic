using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Appointments.AppointmentResult;

namespace Appointments.Write.API.Validators.AppointmentResult
{
    public class CreateAppointmentResultRequestValidator : AbstractValidator<CreateAppointmentResultRequest>
    {
        public CreateAppointmentResultRequestValidator()
        {
            RuleFor(r => r.Complaints).Required();
            RuleFor(r => r.Conclusion).Required();
            RuleFor(r => r.Recommendations).Required();
            RuleFor(r => r.AppointmentId).Required();

            RuleFor(r => r.PatientFullName).Required();
            RuleFor(r => r.PatientDateOfBirth).Required();
            RuleFor(r => r.DoctorFullName).Required();
            RuleFor(r => r.DoctorSpecializationName).Required();
            RuleFor(r => r.ServiceName).Required();
        }
    }
}
