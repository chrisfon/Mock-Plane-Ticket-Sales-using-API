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
    public class FlightsController : ControllerBase
    {
        private readonly FlightsContext _context;

        public FlightsController(FlightsContext context)
        {
            _context = context;
        }

        // GET: api/Flights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
        {
          if (_context.Flights == null)
          {
              return NotFound();
          }
            var lista = await _context.Flights.ToListAsync();
            foreach (var f in lista)
            {
                f.decifrar();
            }
            return lista;
        }

        // GET: api/Flights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(string id)
        {
          if (_context.Flights == null)
          {
              return NotFound();
          }
            id = Cifrado.Cifrar(id);
            var flight = await _context.Flights.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }
            flight.decifrar();
            return flight;
        }

        // PUT: api/Flights/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlight(string id, Flight flight)
        {
            id = Cifrado.Cifrar(id);
            flight.cifrar();
            if (id != flight.Code)
            {
                return BadRequest();
            }

            _context.Entry(flight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(id))
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

        // POST: api/Flights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Flight>> PostFlight(Flight flight)
        {
          if (_context.Flights == null)
          {
              return Problem("Entity set 'FlightsContext.Flights'  is null.");
          }
            flight.cifrar();
            _context.Flights.Add(flight);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FlightExists(flight.Code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFlight", new { id = flight.Code }, flight);
        }

        // DELETE: api/Flights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(string id)
        {
            if (_context.Flights == null)
            {
                return NotFound();
            }
            id = Cifrado.Cifrar(id);
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FlightExists(string id)
        {
            return (_context.Flights?.Any(e => e.Code == id)).GetValueOrDefault();
        }
    }
}
