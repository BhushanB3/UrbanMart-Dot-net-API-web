using AutoMapper;
using UrbanMart.Services.CouponAPI.Models;
using UrbanMart.Services.CouponAPI.Models.DTO;
using UrbanMart.Services.CouponAPI.Services;

namespace UrbanMart.Services.CouponAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Coupon → Result (GetAllCoupons.Result)
            CreateMap<Coupon, CouponDto>();

            CreateMap<CouponDto, Coupon>();
        }

        //public static MapperConfiguration RegisterMaps()
        //{
        //    var mappingConfig = new MapperConfiguration(config =>
        //    {
        //        config.CreateMap<Coupon, GetAllCoupons.Result>();
        //        config.CreateMap<Coupon, GetCouponByCode.Result>();
        //    });
        //    return mappingConfig;
        //}
    }
}
