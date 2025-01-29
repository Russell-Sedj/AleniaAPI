using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AleniaAPI.Data;
using AleniaAPI.Models;

namespace AleniaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtablissementController : ControllerBase
    {
        private readonly AleniaContext _context;

        public EtablissementController(AleniaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Etablissement>>> GetEtablissements()
        {
            return await _context.Etablissements.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Etablissement>> GetEtablissement(Guid id)
        {
            var etablissement = await _context.Etablissements.FindAsync(id);

            if (etablissement == null)
            {
                return NotFound();
            }

            return etablissement;
        }

        [HttpPost]
        public async Task<ActionResult<Etablissement>> PostEtablissement(Etablissement etablissement)
        {
            _context.Etablissements.Add(etablissement);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEtablissement), new { id = etablissement.Id }, etablissement);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEtablissement(Guid id, Etablissement etablissement)
        {
            if (id != etablissement.Id)
            {
                return BadRequest();
            }

            _context.Entry(etablissement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EtablissementExists(id))
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
        public async Task<IActionResult> DeleteEtablissement(Guid id)
        {
            var etablissement = await _context.Etablissements.FindAsync(id);
            if (etablissement == null)
            {
                return NotFound();
            }

            _context.Etablissements.Remove(etablissement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EtablissementExists(Guid id)
        {
            return _context.Etablissements.Any(e => e.Id == id);
        }
    }
}