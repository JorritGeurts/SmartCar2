using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCarAPI.Data;
using SmartCarAPI.Models;

namespace SmartCarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarSeverityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarSeverityController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarServerity()
        {
            var carServ = await _context.CarSeverity.ToListAsync();

            if (carServ == null || !carServ.Any())
            {
                return NotFound("No damages found.");
            }

            return Ok(carServ);
        }

        [HttpPost]
        public async Task<IActionResult> PostCarSeverity(CarSeverity carserv)
        {
            if (carserv == null)
            {
                return BadRequest("Damage data is required.");
            }

            // Create a new Damage entity
            try
            {
                // Add the new car to the context
                _context.CarSeverity.Add(carserv); // Make sure you're adding the car passed in the request
                await _context.SaveChangesAsync(); // Save changes to the database

                // Return the created car with a 201 status code
                return CreatedAtAction(nameof(GetCarServerity), new { id = carserv.Id }, carserv);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                Console.WriteLine($"An error occurred while saving the car: {ex.Message}");
                return StatusCode(500, $"Internal server error {ex.Message}"); // Return a 500 error on failure
            }
        }

    }
}
