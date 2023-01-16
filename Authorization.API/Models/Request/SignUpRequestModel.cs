namespace Authorization.API.Models.Request
{
    public class SignUpRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
