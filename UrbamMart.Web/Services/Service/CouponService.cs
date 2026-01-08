using Microsoft.AspNetCore.Mvc;
using UrbamMart.Web.Models;
using UrbamMart.Web.Models.CouponDto;
using UrbamMart.Web.Services.IService;
using UrbamMart.Web.Utilities;
using static UrbamMart.Web.Utilities.StaticDetails;

namespace UrbamMart.Web.Services.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _IBaseService;
        private readonly string BaseUrl = CouponAPIBase + "/api/Coupon";

        public CouponService(IBaseService baseService)
        {
            _IBaseService = baseService;
        }

        public async Task<ResponseResult?> AddCouponAsync(CouponDto request)
        {
            return await _IBaseService.SendAsync<ResponseResult>(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = request,
                Url = BaseUrl + "/AddCoupon"
            });
        }

        public async Task<ResponseResult?> DeleteCouponAsync(Guid CouponId)
        {
            var result =await _IBaseService.SendAsync<ResponseResult>(new RequestDto()
            {
                ApiType = ApiType.DELETE,
                Url = BaseUrl + "/DeleteCoupon" + $"?id={CouponId}"
            });
            return result;
        }

        public async Task<IEnumerable<CouponDto>?> GetAllCouponAsync(Guid? UserId)
        {
            var url = BaseUrl + "/GetCoupon";
            
            if (UserId.HasValue)
            {
                url += $"?userId = {UserId.Value}";
            }

            return await _IBaseService.SendAsync<IEnumerable<CouponDto>?>(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = url
            });
        }

        public async Task<CouponDto?> GetCouponByCodeAsync(string code)
        {
            return await _IBaseService.SendAsync<CouponDto>(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = CouponAPIBase + BaseUrl + "GetCouponByCode/" + code
            });
        }

        public async Task<ResponseResult?> UpdateCouponAsync(CouponDto request)
        {
            return await _IBaseService.SendAsync<ResponseResult>(new RequestDto()
            {
                ApiType = ApiType.PUT,
                Data = request,
                Url = CouponAPIBase + BaseUrl + "UpdateCoupon"
            });
        }
    }
}
