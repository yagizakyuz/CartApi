using CartApi.Data.Models;

namespace CartApi.Contants
{
    public static class Constants
    {
        public static readonly Dictionary<string, Coupon> ValidCoupons = new Dictionary<string, Coupon>
        {
            { "TimeWax", new Coupon { Id = 1, Code = "TimeWax", DiscountRate = 30}},

            { "HireMe", new Coupon{ Id = 2, Code = "HireMe", DiscountRate = 99}}
        };
    }
}
