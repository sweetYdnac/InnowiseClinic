using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Services.Service;

namespace Services.API.Validators.Service
{
    public class UpdateServiceRequestValidator : AbstractValidator<UpdateServiceRequest>
    {
        public UpdateServiceRequestValidator()
        {
            RuleFor(s => s.Title).Required();
            RuleFor(s => s.Price).Required().GreaterThan(0);
            RuleFor(s => s.SpecializationId).Required();
            RuleFor(s => s.CategoryId).Required();
            RuleFor(s => s.IsActive).NotNull();
        }
    }
}
