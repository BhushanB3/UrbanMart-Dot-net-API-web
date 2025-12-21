using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;
using UrbanMart.Services.CouponAPI.Data;

namespace UrbanMart.Services.CouponAPI.Services
{
    public static class UpdateCoupon
    {
        public class Request : IRequest<Result>
        {
            public Guid Id { get; set; }
            public string? Code { get; set; }
            public double? Discount { get; set; }
            public double? MinimumAmount { get; set; }
            public bool? IsActive { get; set; }

        }

        public class Result
        {
            public HttpStatusCode Statuscode { get; set; }
            public string? Message { get; set; }
        }

        public class Handler : IRequestHandler<Request, Result>
        {
            public readonly ApplicationDBContext _db;
            public readonly IMapper _mapper;

            public Handler(ApplicationDBContext db, IMapper mapper) 
            {
                _db = db;
                _mapper = mapper;
            }
            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {
                var couponInDb = await _db.Coupons.FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeprecated, cancellationToken);
                
                var isUpdated = false;

                if(couponInDb == null)
                {
                    return new Result 
                    { 
                        Statuscode = HttpStatusCode.NotFound,
                        Message = "Coupon Not Found"
                    };
                }
                else
                {
                    if (request.Code != null && request.Code != couponInDb.Code)
                    {
                        couponInDb.Code = request.Code;
                        isUpdated = true;
                    }
                    if(request.Discount.HasValue && request.Discount != couponInDb.Discount)
                    {
                        couponInDb.Discount = request.Discount.Value;
                        isUpdated = true;
                    }
                    if (request.MinimumAmount.HasValue && request.MinimumAmount != couponInDb.MinimumAmount)
                    {
                        couponInDb.MinimumAmount = request.MinimumAmount.Value;
                        isUpdated = true;
                    }
                    if(request.IsActive.HasValue && request.IsActive != couponInDb.IsActive)
                    {
                        couponInDb.IsActive = request.IsActive.Value;
                        isUpdated = true;
                    }

                    if (isUpdated)
                    {
                        couponInDb.ModifiedOn = DateTime.UtcNow;
                        if (await _db.SaveChangesAsync() > 0)
                        {
                            return new Result
                            {
                                Statuscode = HttpStatusCode.OK
                            };
                        }
                    }
                   
                }
                    return new Result();
            }
        }

    }
}
