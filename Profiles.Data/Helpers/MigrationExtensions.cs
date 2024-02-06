using FluentMigrator.Builders.Create.Table;
using Profiles.Data.Entities;

namespace Profiles.Data.Helpers
{
    internal static class MigrationExtensions
    {
        internal static ICreateTableWithColumnSyntax WithUserColumns(this ICreateTableWithColumnSyntax table)
        {
            return table
                .WithColumn(nameof(User.Id)).AsGuid().NotNullable().PrimaryKey()
                .WithColumn(nameof(User.FirstName)).AsString().NotNullable()
                .WithColumn(nameof(User.LastName)).AsString().NotNullable()
                .WithColumn(nameof(User.MiddleName)).AsString().Nullable();
        }
    }
}
