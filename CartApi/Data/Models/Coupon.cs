namespace CartApi.Data.Models
{
    public class Coupon
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public decimal DiscountRate { get; set; }
    }
}
