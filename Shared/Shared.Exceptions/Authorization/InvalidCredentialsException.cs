namespace Shared.Exceptions.Authorization
{
    public class InvalidCredentialsException : ApplicationException
    {
        public InvalidCredentialsException(string message)
            : base(message) { }
    }
}
