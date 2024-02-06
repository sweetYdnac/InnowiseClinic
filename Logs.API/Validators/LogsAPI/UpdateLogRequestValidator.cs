using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.LogsAPI;

namespace Logs.API.Validators.LogsAPI
{
    public class UpdateLogRequestValidator : AbstractValidator<UpdateLogRequest>
    {
        public UpdateLogRequestValidator()
        {
            RuleFor(r => r.ApiName).Required();
            RuleFor(r => r.Route).Required();
            RuleFor(r => r.Code).Required();
            RuleFor(r => r.Message).Required();
            RuleFor(r => r.Details).Required();
        }
    }
}
