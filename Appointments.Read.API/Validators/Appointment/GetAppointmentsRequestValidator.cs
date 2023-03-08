using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Appointments.Appointment;

namespace Appointments.Read.API.Validators.Appointment
{
    public class GetAppointmentsRequestValidator : AbstractValidator<GetAppointmentsRequest>
    {
        public GetAppointmentsRequestValidator()
        {
            RuleFor(p => p.CurrentPage)
                .NotNull()
                .GreaterThan(0);

            RuleFor(p => p.PageSize)
                .NotNull()
                .InclusiveBetween(1, 50);

            RuleFor(p => p.Date).Required();
            RuleFor(p => p.IsApproved).NotNull();
        }
    }
}
