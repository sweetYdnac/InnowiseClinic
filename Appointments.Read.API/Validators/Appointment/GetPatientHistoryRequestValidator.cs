using FluentValidation;
using Shared.Models.Request.Appointments.Appointment;

namespace Appointments.Read.API.Validators.Appointment
{
    public class GetPatientHistoryRequestValidator : AbstractValidator<GetPatientHistoryRequest>
    {
        public GetPatientHistoryRequestValidator()
        {
            RuleFor(p => p.CurrentPage)
                .NotNull()
                .GreaterThan(0);

            RuleFor(p => p.PageSize)
                .NotNull()
                .InclusiveBetween(1, 50);
        }
    }
}
