using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrbanMart.Services.CouponAPI.Data;
using UrbanMart.Services.CouponAPI.Models.DTO;

namespace UrbanMart.Services.CouponAPI.Services
{
    public static class GetCouponByCode
    {
        public class Query: IRequest<CouponDto>
        {
            public required string Code { get; set; }
        }

        public class Handler : IRequestHandler<Query, CouponDto>
        {
            private readonly ApplicationDBContext _db;
            private IMapper _mapper;

            public Handler(ApplicationDBContext db, IMapper mapper)
            {
                _db = db;
                _mapper = mapper;
            }

            public async Task<CouponDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var coupon = await _db.Coupons.FirstOrDefaultAsync(
                                        x => x.Code.ToLower() == request.Code.ToLower() && 
                                        !x.IsDeprecated);
                if(coupon != null)
                {
                    var result = _mapper.Map<CouponDto>(coupon);
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
