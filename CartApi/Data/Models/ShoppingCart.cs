namespace CartApi.Data.Models
{
    public class ShoppingCart
    {
        public long Id { get; set; }

        public Coupon? Coupon { get; set; }

        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
