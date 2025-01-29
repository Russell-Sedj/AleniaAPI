using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AleniaAPI.Data;
using AleniaAPI.Models;

namespace AleniaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatureController : ControllerBase
    {
        private readonly AleniaContext _context;

        public CandidatureController(AleniaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidature>>> GetCandidatures()
        {
            return await _context.Candidatures.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Candidature>> GetCandidature(Guid id)
        {
            var candidature = await _context.Candidatures.FindAsync(id);

            if (candidature == null)
            {
                return NotFound();
            }

            return candidature;
        }

        [HttpPost]
        public async Task<ActionResult<Candidature>> PostCandidature(Candidature candidature)
        {
            _context.Candidatures.Add(candidature);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCandidature), new { id = candidature.Id }, candidature);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidature(Guid id, Candidature candidature)
        {
            if (id != candidature.Id)
            {
                return BadRequest();
            }

            _context.Entry(candidature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidatureExists(id))
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
        public async Task<IActionResult> DeleteCandidature(Guid id)
        {
            var candidature = await _context.Candidatures.FindAsync(id);
            if (candidature == null)
            {
                return NotFound();
            }

            _context.Candidatures.Remove(candidature);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CandidatureExists(Guid id)
        {
            return _context.Candidatures.Any(e => e.Id == id);
        }
    }
}