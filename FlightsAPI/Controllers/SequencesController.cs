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
    public class SequencesController : ControllerBase
    {
        private readonly FlightsContext _context;

        public SequencesController(FlightsContext context)
        {
            _context = context;
        }

        // GET: api/Sequences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sequence>>> GetSequences()
        {
          if (_context.Sequences == null)
          {
              return NotFound();
          }
            return await _context.Sequences.ToListAsync();
        }

        // GET: api/Sequences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sequence>> GetSequence(int id)
        {
          if (_context.Sequences == null)
          {
              return NotFound();
          }
            var sequence = await _context.Sequences.FindAsync(id);

            if (sequence == null)
            {
                return NotFound();
            }

            return sequence;
        }

        // PUT: api/Sequences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSequence(int id, Sequence sequence)
        {
            if (id != sequence.Sequence1)
            {
                return BadRequest();
            }

            _context.Entry(sequence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SequenceExists(id))
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

        // POST: api/Sequences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sequence>> PostSequence(Sequence sequence)
        {
          if (_context.Sequences == null)
          {
              return Problem("Entity set 'FlightsContext.Sequences'  is null.");
          }
            _context.Sequences.Add(sequence);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SequenceExists(sequence.Sequence1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSequence", new { id = sequence.Sequence1 }, sequence);
        }

        // DELETE: api/Sequences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSequence(int id)
        {
            if (_context.Sequences == null)
            {
                return NotFound();
            }
            var sequence = await _context.Sequences.FindAsync(id);
            if (sequence == null)
            {
                return NotFound();
            }

            _context.Sequences.Remove(sequence);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SequenceExists(int id)
        {
            return (_context.Sequences?.Any(e => e.Sequence1 == id)).GetValueOrDefault();
        }
    }
}
