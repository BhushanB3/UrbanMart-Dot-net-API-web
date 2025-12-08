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
        public class Query: IRequest<Result>
        {
            public string Code { get; set; }
        }

        public class Result
        {
            public Guid Id { get; set; }
            public required string Code { get; set; }
            public double Discount { get; set; }
            public bool IsDeprecated { get; set; }
            public bool IsActive { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly ApplicationDBContext _db;
            private IMapper _mapper;

            public Handler(ApplicationDBContext db, IMapper mapper)
            {
                _db = db;
                _mapper = mapper;
            }

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var coupon = await _db.Coupons.FirstOrDefaultAsync(
                                        x => x.Code == request.Code && 
                                        !x.IsDeprecated);
                if(coupon != null)
                {
                    var result = _mapper.Map<Result>(coupon);
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
