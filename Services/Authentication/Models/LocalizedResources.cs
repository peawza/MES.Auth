namespace Authentication.Models
{

    #region LocalizedResources

    public class LocalizedResourcesCriteria
    {

    }
    public class LocalizedResourcesResult
    {
        public string ScreenCode { get; set; }
        public string ObjectID { get; set; }
        public string? ResourcesEN { get; set; }


        public string? ResourcesTH { get; set; }


        public string? Remark { get; set; }

        public DateTime? CreateDate { get; set; }


        public string? CreateBy { get; set; }
    }
    #endregion

}
