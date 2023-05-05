using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightsAPI.Models;

namespace FlightsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlinesController : ControllerBase
    {
        private readonly FlightsContext _context;

        public AirlinesController(FlightsContext context)
        {
            _context = context;
        }

        // GET: api/Airlines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Airline>>> GetAirlines()
        {
          if (_context.Airlines == null)
          {
              return NotFound();
          }
            var data = await _context.Airlines.ToListAsync();
            foreach (var airline in data)
            {
                airline.decifrar();
            }
            return data;
        }

        // GET: api/Airlines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Airline>> GetAirline(string id)
        {
          if (_context.Airlines == null)
          {
              return NotFound();
          }
            id = Cifrado.Cifrar(id);
            var airline = await _context.Airlines.FindAsync(id);

            if (airline == null)
            {
                return NotFound();
            }
            airline.decifrar();
            return airline;
        }

        // PUT: api/Airlines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAirline(string id, Airline airline)
        {
            id = Cifrado.Cifrar(id);
            airline.cifrar();
            if (id != airline.Code)
            {
                return BadRequest();
            }

            _context.Entry(airline).State = EntityState.Modified;

            try
            {
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirlineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Airlines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Airline>> PostAirline(Airline airline)
        {
          if (_context.Airlines == null)
          {
              return Problem("Entity set 'FlightsContext.Airlines'  is null.");
          }
            _context.Airlines.Add(airline);
            try
            {
                airline.cifrar();
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AirlineExists(airline.Code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAirline", new { id = airline.Code }, airline);
        }

        // DELETE: api/Airlines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirline(string id)
        {
            if (_context.Airlines == null)
            {
                return NotFound();
            }
            id = Cifrado.Cifrar(id);
            var airline = await _context.Airlines.FindAsync(id);
            if (airline == null)
            {
                return NotFound();
            }
            //airline.cifrar();
            _context.Airlines.Remove(airline);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AirlineExists(string id)
        {
            return (_context.Airlines?.Any(e => e.Code == id)).GetValueOrDefault();
        }
    }
}
