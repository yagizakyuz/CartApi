namespace CartApi.Controllers.Requests
{
    public class AddToCartRequest
    {
        public long ProductId { get; set; }

        public int Amount { get; set; }
    }
}
