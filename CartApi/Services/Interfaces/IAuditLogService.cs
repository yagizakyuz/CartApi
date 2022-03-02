using CartApi.Controllers.Requests;
using CartApi.Data.Models;

namespace CartApi.Services.Interfaces
{
    public interface IAuditLogService
    {
        Task<List<AuditLog>> GetAuditLogs();

        Task AddAuditLog(AddAuditLogRequest request);
    }
}
