using FluentValidation;

namespace Profiles.API.Extensions
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> Required<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder) =>
            ruleBuilder.NotEmpty().NotNull();
    }
}
