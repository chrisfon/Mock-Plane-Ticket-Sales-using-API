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
    public class CountriesController : ControllerBase
    {
        private readonly FlightsContext _context;

        public CountriesController(FlightsContext context)
        {
            _context = context;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
          if (_context.Countries == null)
          {
              return NotFound();
          }
            var datos =  await _context.Countries.ToListAsync();

            foreach (var country in datos)
            {
               country.decifrar();
            }
            return datos;
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(string id)
        {
          if (_context.Countries == null)
          {
              return NotFound();
          }
            id = Cifrado.Cifrar(id);
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }
            country.decifrar();
            return country;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(string id, Country country)
        {
            id = Cifrado.Cifrar(id);
            country.cifrar();
            if (id != country.Code)
            {
                return BadRequest();
            }

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {
          if (_context.Countries == null)
          {
              return Problem("Entity set 'FlightsContext.Countries'  is null.");
          }
            _context.Countries.Add(country);
            try
            {
                country.cifrar();
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CountryExists(country.Code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCountry", new { id = country.Code }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(string id)
        {
            if (_context.Countries == null)
            {
                return NotFound();
            }
            id = Cifrado.Cifrar(id);
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            //country.cifrar();
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(string id)
        {
            return (_context.Countries?.Any(e => e.Code == id)).GetValueOrDefault();
        }
    }
}
