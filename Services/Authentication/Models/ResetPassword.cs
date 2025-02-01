namespace Authentication.Models
{
    public class ConfirmResetPasswordDo
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
