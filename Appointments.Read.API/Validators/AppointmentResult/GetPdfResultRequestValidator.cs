using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Appointments.AppointmentResult;

namespace Appointments.Read.API.Validators.AppointmentResult
{
    public class GetPdfResultRequestValidator : AbstractValidator<GetPdfResultRequest>
    {
        public GetPdfResultRequestValidator()
        {
            RuleFor(r => r.Date).Required();
            RuleFor(r => r.PatientFullName).Required();
            RuleFor(r => r.PatientDateOfBirth).Required();
            RuleFor(r => r.DoctorFullName).Required();
            RuleFor(r => r.DoctorSpecializationName).Required();
            RuleFor(r => r.ServiceName).Required();
            RuleFor(r => r.Complaints).Required();
            RuleFor(r => r.Conclusion).Required();
            RuleFor(r => r.Recommendations).Required();
        }
    }
}
