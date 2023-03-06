using System.Data;
using static Dapper.SqlMapper;

namespace Profiles.Data.Helpers
{
    public class DateOnlyHandler : TypeHandler<DateOnly>
    {
        public override DateOnly Parse(object value) => DateOnly.FromDateTime((DateTime)value);

        public override void SetValue(IDbDataParameter parameter, DateOnly value)
        {
            parameter.Value = value.ToDateTime(TimeOnly.MinValue);
            parameter.DbType = DbType.Date;
        }
    }
}
