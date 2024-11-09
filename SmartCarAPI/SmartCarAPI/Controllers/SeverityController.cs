using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCarAPI.Data;

namespace SmartCarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeverityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SeverityController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSeverities()
        {
            var severity = await _context.Severity.ToListAsync();

            if (severity == null || !severity.Any())
            {
                return NotFound("Severity not found.");
            }

            return Ok(severity);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeverityById(int id)
        {
            // Attempt to retrieve the CarSeverity by its Id
            var sev = await _context.Severity.FindAsync(id);

            if (sev == null)
            {
                // Return a 404 if the specified Id does not exist
                return NotFound($"CarSeverity with Id {id} not found.");
            }

            // Return the carSeverity object if found
            return Ok(sev);
        }
    }
}
