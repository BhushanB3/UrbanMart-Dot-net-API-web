using System.Net;

namespace UrbamMart.Web.Models.CouponDto
{
    public class ResponseResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
