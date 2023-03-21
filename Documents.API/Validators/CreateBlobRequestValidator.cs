using Documents.API.Extensions;
using FluentValidation;
using Shared.Models.Extensions;
using Shared.Models.Request.Documents;

namespace Documents.API.Validators
{
    public class CreateBlobRequestValidator : AbstractValidator<BlobRequest>
    {
        public CreateBlobRequestValidator()
        {
            RuleFor(p => p.Content)
                .Required()
                .IsBase64String();

            RuleFor(p => p.ContentType).Required();
        }
    }
}
