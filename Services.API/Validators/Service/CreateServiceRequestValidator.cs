using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Services.Service;

namespace Services.API.Validators.Service
{
    public class CreateServiceRequestValidator : AbstractValidator<CreateServiceRequest>
    {
        public CreateServiceRequestValidator()
        {
            RuleFor(s => s.Title).Required();
            RuleFor(s => s.Price).Required().GreaterThan(0);
            RuleFor(s => s.SpecializationId).Required();
            RuleFor(s => s.CategoryId).Required();
            RuleFor(s => s.IsActive).NotNull();

        }
    }
}
