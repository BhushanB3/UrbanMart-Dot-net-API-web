using Microsoft.AspNetCore.Mvc;

namespace UrbanMart.Services.CouponAPI.Models
{
    public class ResponseDTO
    {
        public static IActionResult Success(Object obj, bool isSuccess)
        {
            var result = new Dictionary<string, object>();
            if(obj != null)
            {
                result.Add("data", obj);
            }
            result.Add("success", isSuccess);
            return new OkObjectResult(result);
        }

        public static IActionResult Error(Object obj, bool isSuccess)
        {
            var result = new Dictionary<string, object>();
            if (obj != null)
            {
                result.Add("data", obj);
            }
            result.Add("error", isSuccess);
            return new OkObjectResult(result);
        }
    }
}
