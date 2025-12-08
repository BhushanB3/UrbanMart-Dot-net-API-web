using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrbanMart.Services.CouponAPI.Data;
using UrbanMart.Services.CouponAPI.Services;

namespace UrbanMart.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CouponController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetCoupon")]
        public async Task<ActionResult> GetAllCoupons([FromQuery] Guid? userId)
        {
            var coupons = await _mediator.Send(new GetAllCoupons.Query() { UserId = userId});
            if (coupons == null)
            {
                return NotFound("No coupon for this userId");
            }
            return Ok(coupons);
        }

        [HttpGet]
        [Route("GetCouponByCode")]
        public async Task<ActionResult> GetCouponByCode([FromQuery] string code)
        {
            var coupons = await _mediator.Send(new GetCouponByCode.Query() { Code = code });
            if(coupons == null)
            {
                return NotFound("No coupon by this Id");
            }
            return Ok(coupons);
        }

        [HttpPost]
        [Route("AddCoupon")]
        public async Task<ActionResult> AddCoupon([FromBody] CreateCoupon.Command query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCoupon([FromQuery] Guid id)
        {
            var result = await _mediator.Send(new DeleteCoupon.Command() {  CouponId = id });
            return Ok(result);
        }
    }
}
