using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Documents;

namespace Documents.API.Validators
{
    public class CreateBlobRequestValidator : AbstractValidator<BlobRequest>
    {
        public CreateBlobRequestValidator()
        {
            RuleFor(p => p.Content).Required();
            RuleFor(p => p.ContentType).Required();
        }
    }
}
