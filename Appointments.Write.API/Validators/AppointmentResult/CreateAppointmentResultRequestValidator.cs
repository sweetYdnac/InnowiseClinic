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
            RuleFor(r => r.Recomendations).Required();
            RuleFor(r => r.AppointmentId).Required();
        }
    }
}
