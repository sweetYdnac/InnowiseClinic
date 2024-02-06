using FluentValidation;
using Shared.Models.Request.LogsAPI;

namespace Logs.API.Validators.LogsAPI
{
    public class GetLogsRequestValidator : AbstractValidator<GetLogsRequest>
    {
        public GetLogsRequestValidator()
        {
            RuleFor(r => r.CurrentPage)
                .NotNull()
                .GreaterThan(0);

            RuleFor(r => r.PageSize)
                .NotNull()
                .InclusiveBetween(1, 50);
        }
    }
}
