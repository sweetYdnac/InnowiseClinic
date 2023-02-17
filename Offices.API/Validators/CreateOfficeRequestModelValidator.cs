using FluentValidation;
using Shared.Models.Request.Offices;

namespace Offices.API.Validators
{
    public class CreateOfficeRequestModelValidator : AbstractValidator<CreateOfficeRequest>
    {
        public CreateOfficeRequestModelValidator()
        {
            RuleFor(m => m.City)
                .NotEmpty()
                .NotNull();

            RuleFor(m => m.Street)
                .NotEmpty()
                .NotNull();

            RuleFor(m => m.HouseNumber)
                .NotEmpty()
                .NotNull();

            RuleFor(m => m.OfficeNumber)
                .NotEmpty()
                .NotNull();

            RuleFor(m => m.RegistryPhoneNumber)
                .NotEmpty()
                .NotNull();

            RuleFor(m => m.IsActive)
                .NotNull();
        }
    }
}
