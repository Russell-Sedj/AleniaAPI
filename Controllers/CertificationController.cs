using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AleniaAPI.Data;
using AleniaAPI.Models;

namespace AleniaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificationController : ControllerBase
    {
        private readonly AleniaContext _context;

        public CertificationController(AleniaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Certification>>> GetCertifications()
        {
            return await _context.Certifications.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Certification>> GetCertification(Guid id)
        {
            var certification = await _context.Certifications.FindAsync(id);

            if (certification == null)
            {
                return NotFound();
            }

            return certification;
        }

        [HttpPost]
        public async Task<ActionResult<Certification>> PostCertification(Certification certification)
        {
            _context.Certifications.Add(certification);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCertification), new { id = certification.Id }, certification);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCertification(Guid id, Certification certification)
        {
            if (id != certification.Id)
            {
                return BadRequest();
            }

            _context.Entry(certification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificationExists(id))
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
        public async Task<IActionResult> DeleteCertification(Guid id)
        {
            var certification = await _context.Certifications.FindAsync(id);
            if (certification == null)
            {
                return NotFound();
            }

            _context.Certifications.Remove(certification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CertificationExists(Guid id)
        {
            return _context.Certifications.Any(e => e.Id == id);
        }
    }
}