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
    public class UsersController : ControllerBase
    {
        private readonly FlightsContext _context;

        public UsersController(FlightsContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            
            var listausuarios = await _context.Users.ToListAsync();
            foreach (var user in listausuarios)
            {
                user.decifrar();
            }
            return listausuarios;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            id = Cifrado.Cifrar(id);
            var user = await _context.Users.FindAsync(id);
            
            if (user == null)
            {
                return NotFound();
            }
            user.decifrar();
            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            user.cifrar();
            id = Cifrado.Cifrar(id);
            if (id != user.UserName)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'FlightsContext.Users'  is null.");
          }
            _context.Users.Add(user);
          
            try
            {
                user.cifrar();
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                if (UserExists(user.UserName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.UserName }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            id = Cifrado.Cifrar(id);
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            //user.cifrar();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return (_context.Users?.Any(e => e.UserName == id)).GetValueOrDefault();
        }
    }
}
