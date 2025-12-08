using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UrbanMart.Services.CouponAPI.Data;
using UrbanMart.Services.CouponAPI.Models;

namespace UrbanMart.Services.CouponAPI.Services
{
    public static class CreateCoupon
    {
        public class Command : IRequest<Result>
        {
            public required string Code { get; set; }
            public double Discount { get; set; }
        }

        public class Result
        {
            public HttpStatusCode code;
            public string? Message { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly ApplicationDBContext _db;

            public Handler(ApplicationDBContext db)
            {
                _db = db;
            }

            public async Task<Result> Handle(Command message, CancellationToken cancellationToken)
            {
                var checkExistingCoupon = await (from coupon in _db.Coupons
                                                 where coupon.Code == message.Code
                                                 && !coupon.IsDeprecated
                                                 select coupon).AnyAsync(cancellationToken);

                if (checkExistingCoupon)
                {
                    return new Result
                    {
                        code = HttpStatusCode.Conflict,
                        Message = "Coupon already exists"
                    };
                }

                _db.Coupons.Add(new Coupon
                {
                    Code = message.Code,
                    Discount = message.Discount,
                    IsActive = true,
                    IsDeprecated = false,
                    ModifiedOn = DateTime.UtcNow
                });

                if (await _db.SaveChangesAsync() > 0)
                {
                    return new Result
                    {
                        code = HttpStatusCode.Created,
                    };
                }
                return new Result();
            }
        }
    }
}
