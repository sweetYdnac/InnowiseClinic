namespace Shared.Core.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetFullMessage(this Exception ex)
        {
            return ex.InnerException is not null
                ? $"{ex.Message}; {GetFullMessage(ex.InnerException)}"
                : $"{ex.Message}";
        }
    }
}
