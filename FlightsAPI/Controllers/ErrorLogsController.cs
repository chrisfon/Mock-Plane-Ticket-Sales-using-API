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
    public class ErrorLogsController : ControllerBase
    {
        private readonly FlightsContext _context;

        public ErrorLogsController(FlightsContext context)
        {
            _context = context;
        }

        // GET: api/ErrorLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ErrorLog>>> GetErrorLogs()
        {
          if (_context.ErrorLogs == null)
          {
              return NotFound();
          }
           var data = await _context.ErrorLogs.ToListAsync();

           foreach (var errorLog in data)
            {
                errorLog.decifrar();
            }
            return data;
        }

        // GET: api/ErrorLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ErrorLog>> GetErrorLog(long id)
        {
          if (_context.ErrorLogs == null)
          {
              return NotFound();
          }
            var errorLog = await _context.ErrorLogs.FindAsync(id);

            if (errorLog == null)
            {
                return NotFound();
            }
            errorLog.decifrar();
            return errorLog;
        }

        // PUT: api/ErrorLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutErrorLog(long id, ErrorLog errorLog)
        {
            if (id != errorLog.Id)
            {
                return BadRequest();
            }
            errorLog.cifrar();
            _context.Entry(errorLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ErrorLogExists(id))
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

        // POST: api/ErrorLogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ErrorLog>> PostErrorLog(ErrorLog errorLog)
        {
          if (_context.ErrorLogs == null)
          {
              return Problem("Entity set 'FlightsContext.ErrorLogs'  is null.");
          }
            _context.ErrorLogs.Add(errorLog);
            try
            {
                errorLog.cifrar();
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ErrorLogExists(errorLog.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetErrorLog", new { id = errorLog.Id }, errorLog);
        }

        // DELETE: api/ErrorLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteErrorLog(long id)
        {
            if (_context.ErrorLogs == null)
            {
                return NotFound();
            }
            //id = Cifrado.Cifrar(id);
            var errorLog = await _context.ErrorLogs.FindAsync(id);
            if (errorLog == null)
            {
                return NotFound();
            }
            errorLog.cifrar();
            _context.ErrorLogs.Remove(errorLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ErrorLogExists(long id)
        {
            return (_context.ErrorLogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
