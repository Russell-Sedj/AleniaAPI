using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AleniaAPI.Data;
using AleniaAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace AleniaAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InterimaireController : ControllerBase
    {
        private readonly AleniaContext _context;

        public InterimaireController(AleniaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Interimaire>>> GetInterimaires()
        {
            return await _context.Interimaires.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Interimaire>> GetInterimaire(Guid id)
        {
            var interimaire = await _context.Interimaires.FindAsync(id);

            if (interimaire == null)
            {
                return NotFound();
            }

            return interimaire;
        }

        [HttpPost]
        public async Task<ActionResult<Interimaire>> PostInterimaire(Interimaire interimaire)
        {
            _context.Interimaires.Add(interimaire);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInterimaire), new { id = interimaire.Id }, interimaire);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInterimaire(Guid id, Interimaire interimaire)
        {
            if (id != interimaire.Id)
            {
                return BadRequest();
            }

            _context.Entry(interimaire).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterimaireExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterimaire(Guid id)
        {
            var interimaire = await _context.Interimaires.FindAsync(id);
            if (interimaire == null)
            {
                return NotFound();
            }

            _context.Interimaires.Remove(interimaire);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InterimaireExists(Guid id)
        {
            return _context.Interimaires.Any(e => e.Id == id);
        }
    }
}