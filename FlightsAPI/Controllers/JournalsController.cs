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
    public class JournalsController : ControllerBase
    {
        private readonly FlightsContext _context;

        public JournalsController(FlightsContext context)
        {
            _context = context;
        }

        // GET: api/Journals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Journal>>> GetJournals()
        {
          if (_context.Journals == null)
          {
              return NotFound();
          }
            var data = await _context.Journals.ToListAsync();
            foreach (var journal in data)
            {
                journal.decifrar();
            }
            return data;
        }

        // GET: api/Journals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Journal>> GetJournal(long id)
        {
          if (_context.Journals == null)
          {
              return NotFound();
          }
            var journal = await _context.Journals.FindAsync(id);

            if (journal == null)
            {
                return NotFound();
            }
            journal.decifrar();
            return journal;
        }

        // PUT: api/Journals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJournal(long id, Journal journal)
        {
            if (id != journal.Id)
            {
                return BadRequest();
            }

            _context.Entry(journal).State = EntityState.Modified;

            try
            {
                journal.cifrar();
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JournalExists(id))
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

        // POST: api/Journals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Journal>> PostJournal(Journal journal)
        {
          if (_context.Journals == null)
          {
              return Problem("Entity set 'FlightsContext.Journals'  is null.");
          }
            _context.Journals.Add(journal);
            try
            {
                journal.cifrar();
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JournalExists(journal.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJournal", new { id = journal.Id }, journal);
        }

        // DELETE: api/Journals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJournal(long id)
        {
            if (_context.Journals == null)
            {
                return NotFound();
            }
           
            var journal = await _context.Journals.FindAsync(id);
            if (journal == null)
            {
                return NotFound();
            }

            _context.Journals.Remove(journal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JournalExists(long id)
        {
            return (_context.Journals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
