using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UrbanMart.Services.CouponAPI.Services;

namespace UrbanMart.Services.CouponAPI.Controllers
{
    [Route("api/Coupon")] // base URL
    [ApiController] // tells ASP.NET Core that this is an API controller
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
            var result = await _mediator.Send(new GetAllCoupons.Query() { UserId = userId});
            if (result == null)
            {
                return NotFound("No coupon for this userId");
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetCouponByCode")]
        public async Task<ActionResult> GetCouponByCode([FromQuery] string code)
        {
            var result = await _mediator.Send(new GetCouponByCode.Query() { Code = code });
            if(result == null)
            {
                return NotFound("No coupon by this Id");
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("AddCoupon")]
        public async Task<ActionResult> AddCoupon([FromBody] CreateCoupon.Command query)
        {
            var result = await _mediator.Send(query);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCoupon([FromQuery] Guid id)
        {
            var result = await _mediator.Send(new DeleteCoupon.Command() {  CouponId = id });
            return StatusCode((int)result.StatusCode, result);

        }
    }
}
