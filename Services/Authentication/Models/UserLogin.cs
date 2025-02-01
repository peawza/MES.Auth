namespace Authentication.Models
{
    public class UserLoginDo
    {
        public string AppCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
        public string VerifyCode { get; set; }
    }
    public class ErrorConCurrentResponse
    {
        public ErrorConCurrentList[] errors { get; set; }
    }

    public class ErrorConCurrentList
    {
        public string message { get; set; }
    }
}
