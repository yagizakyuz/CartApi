using CartApi.Data.Enums;

namespace CartApi.Data.Models
{
    public class AuditLog
    {
        public Guid Id { get; set; }

        public ShoppingCartAction ShoppingCartAction { get; set; }

        public long ProductId { get; set; }

        public int Amount { get; set; }

        public DateTime FiredAt { get; set; }
    }
}