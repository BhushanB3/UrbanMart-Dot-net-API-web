using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UrbanMart.Services.CouponAPI.Data;
using UrbanMart.Services.CouponAPI.Models.DTO;

namespace UrbanMart.Services.CouponAPI.Services
{
    public static class GetAllCoupons 
    {
        public class Query: IRequest<IEnumerable<Result>>
        {
            public Guid? UserId { get; set; }
        }

        public class Result
        {
            public Guid Id { get; set; }
            public required string Code { get; set; }
            public double Discount { get; set; }
            public bool IsDeprecated { get; set; }
            public bool IsActive { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<Result>>
        {
            private readonly ApplicationDBContext _db;
            private IMapper _mapper;

            public Handler(ApplicationDBContext db, IMapper mapper)
            {
                _db = db;
                _mapper = mapper;
            }

            public async Task<IEnumerable<Result>> Handle(Query request, CancellationToken cancellationToken)
            {
                var couponList = await _db.Coupons.Where(x=> !x.IsDeprecated).ToListAsync(cancellationToken);
                if(couponList != null && couponList.Count != 0)
                {
                    var result = _mapper.Map<IEnumerable<Result>>(couponList);
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
