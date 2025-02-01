namespace Authentication.Models
{
    public class UserLogTrailDo
    {
        public string? action_name { get; set; }
        public string? data_before { get; set; }
        public string? data_after { get; set; }
        public string? allowed_service { get; set; }
        public DateTime create_date { get; set; }
        public string? create_user { get; set; }

        public string? username { get; set; }
    }

    public class UserLogTrailCriteriaDo
    {
        public string? username { get; set; }
        public DateTime date_from { get; set; }
        public DateTime date_to { get; set; }

    }
}
