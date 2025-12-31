using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using UrbamMart.Web.Models.CouponDto;
using UrbamMart.Web.Services.IService;

namespace UrbamMart.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> CouponList()
        {
            var result = await _couponService.GetAllCouponAsync(null);
            if(result == null)
            {
                TempData["error"] = "Something went wrong";
            }
                return View(result);
        }

        public async Task<IActionResult> CreateCoupon()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon(CouponDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _couponService.AddCouponAsync(model);
                if(response != null && response.StatusCode == HttpStatusCode.Created)
                {
                    TempData["success"] = "Coupon added successfully";
                    return RedirectToAction(nameof(CouponList));
                }
                else
                {
                    TempData["error"] = response.Message;
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCoupon(Guid id)
        {
            if (ModelState.IsValid)
            {
                var result = await _couponService.DeleteCouponAsync(id);
                if(result != null && result.StatusCode == HttpStatusCode.Gone)
                {
                    TempData["success"] = "Coupon deleted";
                    return RedirectToAction(nameof(CouponList));
                }
                else if( result != null && result.StatusCode == HttpStatusCode.NotFound)
                {
                    TempData["error"] = "Coupon not found";
                    return RedirectToAction(nameof(CouponList));
                }
            }
            return View();
        }
    }
}
