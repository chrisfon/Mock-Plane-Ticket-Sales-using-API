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
    public class CardsController : ControllerBase
    {
        private readonly FlightsContext _context;

        public CardsController(FlightsContext context)
        {
            _context = context;
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCards()
        {
          if (_context.Cards == null)
          {
              return NotFound();
          }
            var data =  await _context.Cards.ToListAsync();
            foreach (var card in data)
            {
                card.decifrar();
            }
            return data;
        }

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(string id)
        {
          if (_context.Cards == null)
          {
              return NotFound();
          }
            var card = await _context.Cards.FindAsync(id);

            if (card == null)
            {
                return NotFound();
            }
            card.decifrar();
            return card;
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(string id, Card card)
        {
            if (id != card.Pan)
            {
                return BadRequest();
            }

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                card.cifrar();
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
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

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(Card card)
        {
          if (_context.Cards == null)
          {
              return Problem("Entity set 'FlightsContext.Cards'  is null.");
          }
            _context.Cards.Add(card);
            try
            {
                card.cifrar();
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CardExists(card.Pan))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCard", new { id = card.Pan }, card);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(string id)
        {
            if (_context.Cards == null)
            {
                return NotFound();
            }
            id = Cifrado.Cifrar(id);
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            //card.cifrar();
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardExists(string id)
        {
            return (_context.Cards?.Any(e => e.Pan == id)).GetValueOrDefault();
        }
    }
}
