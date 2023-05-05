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
    public class DoorsController : ControllerBase
    {
        private readonly FlightsContext _context;

        public DoorsController(FlightsContext context)
        {
            _context = context;
        }

        // GET: api/Doors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Door>>> GetDoors()
        {
          if (_context.Doors == null)
          {
              return NotFound();
          }
            var data = await _context.Doors.ToListAsync();

            foreach (var door in data)
            {
                door.decifrar();
            }
            return data;
        }

        // GET: api/Doors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Door>> GetDoor(string id)
        {
          if (_context.Doors == null)
          {
              return NotFound();
          }
            id = Cifrado.Cifrar(id);
            var door = await _context.Doors.FindAsync(id);

            if (door == null)
            {
                return NotFound();
            }
            door.decifrar();
            return door;
        }

        // PUT: api/Doors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoor(string id, Door door)
        {
            id = Cifrado.Cifrar(id);
            door.cifrar();
            if (id != door.Code)
            {
                return BadRequest();
            }

            _context.Entry(door).State = EntityState.Modified;

            try
            {
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoorExists(id))
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

        // POST: api/Doors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Door>> PostDoor(Door door)
        {
          if (_context.Doors == null)
          {
              return Problem("Entity set 'FlightsContext.Doors'  is null.");
          }
            _context.Doors.Add(door);
            try
            {
                door.cifrar();
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DoorExists(door.Code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDoor", new { id = door.Code }, door);
        }

        // DELETE: api/Doors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoor(string id)
        {
            if (_context.Doors == null)
            {
                return NotFound();
            }
            id = Cifrado.Cifrar(id);
            var door = await _context.Doors.FindAsync(id);
            if (door == null)
            {
                return NotFound();
            }
            //door.cifrar();
            _context.Doors.Remove(door);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoorExists(string id)
        {
            return (_context.Doors?.Any(e => e.Code == id)).GetValueOrDefault();
        }
    }
}
