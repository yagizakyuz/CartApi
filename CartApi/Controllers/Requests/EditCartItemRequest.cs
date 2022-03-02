namespace CartApi.Controllers.Requests
{
    public class EditCartItemRequest
    {
        public long ProductId { get; set; }

        public int NewAmount { get; set; }
    }
}
