namespace UrbanMart.Services.CouponAPI.Models.DTO
{
    public class CouponDto
    {
        public Guid Id { get; set; }
        public required string Code { get; set; }
        public double Discount { get; set; }
        public double MinimumAmount { get; set; }
        public bool IsDeprecated { get; set; }
        public bool IsActive { get; set; }
    }
}
