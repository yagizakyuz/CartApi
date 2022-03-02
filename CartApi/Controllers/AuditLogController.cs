using Microsoft.AspNetCore.Mvc;
using CartApi.Data.Models;
using CartApi.Services.Interfaces;

namespace CartApi.Controllers
{
    [ApiController]
    [Route("audit-log")]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;

        public AuditLogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuditLog()
        {
            List<AuditLog> auditLogs = await _auditLogService.GetAuditLogs();

            return Ok(auditLogs);
        }
    }
}