using CartApi.Data.Enums;

namespace CartApi.Controllers.Requests
{
    public class AddAuditLogRequest
    {
        public long ProductId { get; set; }

        public int Amount { get; set; }

        public ShoppingCartAction ShoppingCartAction { get; set; }
    }
}
