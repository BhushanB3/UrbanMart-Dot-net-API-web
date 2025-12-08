using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UrbanMart.Services.CouponAPI.Models
{
    public class Coupon
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Code{ get; set; }
        [Required]
        public double Discount{ get; set; }
        public bool IsDeprecated{ get; set; }
        public bool IsActive{ get; set; }
        public DateTime CreatedOn{ get; set; }
        public DateTime ModifiedOn{ get; set; }
    }
}
