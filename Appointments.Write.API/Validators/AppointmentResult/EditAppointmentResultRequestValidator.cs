using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Appointments.AppointmentResult;

namespace Appointments.Write.API.Validators.AppointmentResult
{
    public class EditAppointmentResultRequestValidator : AbstractValidator<EditAppointmentResultRequest>
    {
        public EditAppointmentResultRequestValidator()
        {
            RuleFor(r => r.Complaints).Required();
            RuleFor(r => r.Conclusion).Required();
            RuleFor(r => r.Recomendations).Required();
        }
    }
}
