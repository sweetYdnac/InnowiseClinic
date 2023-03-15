using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Appointments.Appointment;

namespace Appointments.Read.API.Validators.Appointment
{
    public class GetTimeSlotsRequestValidator : AbstractValidator<GetTimeSlotsRequest>
    {
        public GetTimeSlotsRequestValidator()
        {
            RuleFor(r => r.Date)
                .Required()
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now));

            RuleFor(r => r.ServiceId).Required();
            RuleFor(r => r.StartTime).Required();
            RuleFor(r => r.EndTime).Required();
            RuleFor(r => r.Duration)
                .Required()
                .GreaterThan(0)
                .Must(p => p % 10 == 0)
                .WithMessage("Time slot duration should be divided by 10.");
        }
    }
}
