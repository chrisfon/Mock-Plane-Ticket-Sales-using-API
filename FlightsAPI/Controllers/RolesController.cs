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
    public class RolesController : ControllerBase
    {
        private readonly FlightsContext _context;

        public RolesController(FlightsContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
          if (_context.Roles == null)
          {
              return NotFound();
          }
            var datos = await _context.Roles.ToListAsync();
            foreach (var role in datos)
            {
                role.decifrar();
            }
            return datos;
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(string id)
        {
          if (_context.Roles == null)
          {
              return NotFound();
          }
            id = Cifrado.Cifrar(id);
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }
            role.decifrar();
            return role;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(string id, Role role)
        {
            id = Cifrado.Cifrar(id);
            role.cifrar();
            if (id != role.Id)
            {
                return BadRequest();
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
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

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
          
            if (_context.Roles == null)
          {
              return Problem("Entity set 'FlightsContext.Roles'  is null.");
          }
            _context.Roles.Add(role);
            try
            {
                role.cifrar();
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RoleExists(role.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRole", new { id = role.Id }, role);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (_context.Roles == null)
            {
                return NotFound();
            }
            id = Cifrado.Cifrar(id);
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            //role.cifrar();
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleExists(string id)
        {
            return (_context.Roles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
