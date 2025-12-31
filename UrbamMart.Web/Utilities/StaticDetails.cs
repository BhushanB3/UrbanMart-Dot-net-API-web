namespace UrbamMart.Web.Utilities
{
    public class StaticDetails
    {
        public static string CouponAPIBase { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            DELETE,
            PUT
        }
    }
}
