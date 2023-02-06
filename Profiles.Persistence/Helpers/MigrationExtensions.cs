using FluentMigrator.Builders.Create.Table;
using Profiles.Domain.Entities;

namespace Profiles.Persistence.Helpers
{
    internal static class MigrationExtensions
    {
        internal static ICreateTableWithColumnSyntax WithUserColumns(this ICreateTableWithColumnSyntax table)
        {
            return table
                .WithColumn(nameof(User.Id)).AsGuid().NotNullable().PrimaryKey()
                .WithColumn(nameof(User.FirstName)).AsString().NotNullable()
                .WithColumn(nameof(User.LastName)).AsString().NotNullable()
                .WithColumn(nameof(User.MiddleName)).AsString().Nullable()
                .WithColumn(nameof(User.AccountId)).AsGuid().Nullable().Unique();
        }
    }
}
