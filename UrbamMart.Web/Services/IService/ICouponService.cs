using Microsoft.AspNetCore.Mvc;
using UrbamMart.Web.Models.CouponDto;

namespace UrbamMart.Web.Services.IService
{
    public interface ICouponService
    {
        Task<IEnumerable<CouponDto>?> GetAllCouponAsync(Guid? UserId);
        Task<CouponDto?> GetCouponByCodeAsync(string code);
        Task<ResponseResult?> AddCouponAsync(CouponDto request);
        Task<ResponseResult?> UpdateCouponAsync(CouponDto request);
        Task<ResponseResult?> DeleteCouponAsync(Guid CouponId);
    }
}
