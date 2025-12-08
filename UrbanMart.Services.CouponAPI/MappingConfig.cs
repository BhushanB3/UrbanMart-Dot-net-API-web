using AutoMapper;
using UrbanMart.Services.CouponAPI.Models;
using UrbanMart.Services.CouponAPI.Services;

namespace UrbanMart.Services.CouponAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Coupon → Result (GetAllCoupons.Result)
            CreateMap<Coupon, GetAllCoupons.Result>();
            CreateMap<Coupon, GetCouponByCode.Result>();
        }
    }
}
