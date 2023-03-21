using FluentValidation;

namespace Documents.API.Extensions
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> IsBase64String<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Must(IsBase64String)
                .WithMessage("String must be converted ToBase64String");
        }

        private static bool IsBase64String(string base64)
        {
            var buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out var bytesParsed);
        }
    }
}
