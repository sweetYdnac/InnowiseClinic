namespace Shared.Exceptions.Authorization
{
    public class AccountInactiveException : ApplicationException
    {
        public AccountInactiveException(string message) 
            : base(message) { }
    }
}
