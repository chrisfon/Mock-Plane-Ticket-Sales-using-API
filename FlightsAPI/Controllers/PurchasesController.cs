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
    public class PurchasesController : ControllerBase
    {
        private readonly FlightsContext _context;

        public PurchasesController(FlightsContext context)
        {
            _context = context;
        }

        // GET: api/Purchases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchases()
        {
          if (_context.Purchases == null)
          {
              return NotFound();
          }
            var lista = await _context.Purchases.ToListAsync();
            foreach (var f in lista)
            {
                f.decifrar();
            }
            return lista;
        }

        // GET: api/Purchases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Purchase>> GetPurchase(string id)
        {
          if (_context.Purchases == null)
          {
              return NotFound();
          }
            var purchase = await _context.Purchases.FindAsync(id);

            if (purchase == null)
            {
                return NotFound();
            }
            purchase.decifrar();
            return purchase;
        }

        // PUT: api/Purchases/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchase(string id, Purchase purchase)
        {
            if (id != purchase.Code)
            {
                return BadRequest();
            }

            _context.Entry(purchase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExists(id))
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

        // POST: api/Purchases
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Purchase>> PostPurchase(Purchase purchase)
        {
          if (_context.Purchases == null)
          {
              return Problem("Entity set 'FlightsContext.Purchases'  is null.");
          }
            purchase.cifrar();
            _context.Purchases.Add(purchase);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PurchaseExists(purchase.Code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPurchase", new { id = purchase.Code }, purchase);
        }

        // DELETE: api/Purchases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(string id)
        {
            if (_context.Purchases == null)
            {
                return NotFound();
            }
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }

            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurchaseExists(string id)
        {
            return (_context.Purchases?.Any(e => e.Code == id)).GetValueOrDefault();
        }
    }
}
