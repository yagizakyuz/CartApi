using CartApi.Controllers.Requests;
using CartApi.Data.Models;
using CartApi.Services.Interfaces;

namespace CartApi.Services
{
    public class AuditLogService : IAuditLogService
    {
        public static List<AuditLog> AuditLogs = new List<AuditLog>();

        public async Task AddAuditLog(AddAuditLogRequest request)
        {
            AuditLog auditLog = new AuditLog
            {
                Amount = request.Amount,
                FiredAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                ProductId = request.ProductId,
                ShoppingCartAction = request.ShoppingCartAction
            };

            AuditLogs.Add(auditLog);
        }

        public async Task<List<AuditLog>> GetAuditLogs()
        {
            return AuditLogs;
        }
    }
}
