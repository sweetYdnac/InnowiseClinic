using Swashbuckle.AspNetCore.Filters;

namespace Shared.Models.Request.Documents.SwaggerExamples
{
    public class BlobRequestExample : IExamplesProvider<BlobRequest>
    {
        public BlobRequest GetExamples() =>
            new()
            {
                Content = "some array of bytes converted to string",
                ContentType = "image/jpeg",
            };
    }
}
