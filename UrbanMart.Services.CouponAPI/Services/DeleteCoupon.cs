using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UrbanMart.Services.CouponAPI.Data;

namespace UrbanMart.Services.CouponAPI.Services
{
    public static class DeleteCoupon
    {
        public class Command : IRequest<Result>
        {
            public Guid CouponId { get; set; }
        }
        public class Result
        {
            public HttpStatusCode StatusCode { get; set; }
            public string? Message {  get; set; }
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
                var coupon = await (from cou in _db.Coupons
                                    where cou.Id == message.CouponId
                                    && !cou.IsDeprecated
                                    select cou).FirstOrDefaultAsync(cancellationToken);
                if (coupon == null)
                {
                    return new Result
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Coupon Not found"
                    };
                }
                
                coupon.IsDeprecated = true;
                coupon.ModifiedOn = DateTime.UtcNow;

                if(await _db.SaveChangesAsync() > 0)
                {
                    return new Result
                    {
                        StatusCode = HttpStatusCode.NoContent,
                        Message = "Deleted"
                    };
                }
                return new Result();
            }
        }
    }
}
