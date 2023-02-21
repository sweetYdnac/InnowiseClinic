namespace Shared.Exceptions.Authorization
{
    public class AccountNotCreatedException : ApplicationException
    {
        public AccountNotCreatedException(string message)
            : base(message) { }
    }
}
