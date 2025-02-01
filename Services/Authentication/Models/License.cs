namespace Authentication.Models
{
    public class LicenseDo
    {
        public Guid license_id { get; set; }
        public string license_code { get; set; }
        public string license_name { get; set; }
        public short max_concurrent { get; set; }
        public string allowed_service { get; set; }
        public DateTime expiration_date { get; set; }
        public DateTime create_date { get; set; }
        public DateTime? update_date { get; set; }
        public string? license_type { get; set; }
    }
}
