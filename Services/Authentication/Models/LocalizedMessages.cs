namespace Authentication.Models
{

    #region LocalizedMessages
    public class LocalizedMessagesCriteria
    {

    }

    public class LocalizedMessagesResult
    {
        public string MessageCode { get; set; }
        public string MessageType { get; set; }
        public string? MessageNameEN { get; set; }
        public string? MessageNameTH { get; set; }
        public string? Remark { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreateBy { get; set; }
    }
    #endregion



}
