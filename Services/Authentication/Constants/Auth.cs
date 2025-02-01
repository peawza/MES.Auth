namespace Authentication.Constants
{
    public class AUTH
    {
        public static int LOGIN_WAITING_TIME { get; private set; }
        public static int MAXIMUM_LOGIN_FAIL { get; private set; }
        public static bool AUTO_GENERATE_USER { get; private set; }
        public static string? REMEMBER_USER_NAME { get; private set; }
        public static string? REMEMBER_USER_KEY { get; private set; }
        public static int? REMEMBER_COOKIE_EXPIRE_DATE { get; private set; }
        public static int? REMIND_PASSWORD_DATE { get; private set; }
        public static int REFRESH_TOKEN_EXPIRE_MINUTES { get; private set; }
        public static int DATA_PROTECTION_TOKEN_TIMEOUT_MINUTES { get; private set; }
        public static string RESET_PASSWORD_URL { get; private set; }
    }
    public class ROLE
    {
        public static readonly string CLAIM_TYPE_PERMISSION = "Permission";
        public static readonly string PERMISSION_OPEN = "OPN";
    }

}
