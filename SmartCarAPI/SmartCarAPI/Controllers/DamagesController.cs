using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCarAPI.Data;
using SmartCarAPI.Models;

namespace SmartCarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DamagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DamagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDamages()
        {
            var damages = await _context.Damage.ToListAsync();

            if (damages == null || !damages.Any())
            {
                return NotFound("No damages found.");
            }

            return Ok(damages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDamageById(int id)
        {
            // Attempt to retrieve the CarSeverity by its Id
            var damage = await _context.Damage.FindAsync(id);

            if (damage == null)
            {
                // Return a 404 if the specified Id does not exist
                return NotFound($"CarSeverity with Id {id} not found.");
            }

            // Return the carSeverity object if found
            return Ok(damage);
        }
    }
}
