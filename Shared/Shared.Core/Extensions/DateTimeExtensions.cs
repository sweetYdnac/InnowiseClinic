namespace Shared.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime Ceiling(this DateTime dt, TimeSpan step)
        {
            return new DateTime((dt.Ticks + step.Ticks - 1) / step.Ticks * step.Ticks, dt.Kind);
        }
    }
}
