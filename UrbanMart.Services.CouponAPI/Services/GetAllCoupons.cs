using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UrbanMart.Services.CouponAPI.Data;
using UrbanMart.Services.CouponAPI.Models.DTO;

namespace UrbanMart.Services.CouponAPI.Services
{
    public static class GetAllCoupons 
    {
        public class Query: IRequest<IEnumerable<CouponDto>>
        {
            public Guid? UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<CouponDto>>
        {
            private readonly ApplicationDBContext _db;
            private IMapper _mapper;

            public Handler(ApplicationDBContext db, IMapper mapper)
            {
                _db = db;
                _mapper = mapper;
            }

            public async Task<IEnumerable<CouponDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var couponList = await _db.Coupons.Where(x=> !x.IsDeprecated).ToListAsync(cancellationToken);
                if(couponList != null && couponList.Count != 0)
                {
                    var result = _mapper.Map<IEnumerable<CouponDto>>(couponList);
                    return result;
                }
                else
                {
                    return Enumerable.Empty<CouponDto>();
                }
            }
        }
    }
}
