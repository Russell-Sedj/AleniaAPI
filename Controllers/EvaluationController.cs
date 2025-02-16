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
    public class EvaluationController : ControllerBase
    {
        private readonly AleniaContext _context;

        public EvaluationController(AleniaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evaluation>>> GetEvaluations()
        {
            return await _context.Evaluations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Evaluation>> GetEvaluation(Guid id)
        {
            var evaluation = await _context.Evaluations.FindAsync(id);

            if (evaluation == null)
            {
                return NotFound();
            }

            return evaluation;
        }

        [HttpPost]
        public async Task<ActionResult<Evaluation>> PostEvaluation(Evaluation evaluation)
        {
            _context.Evaluations.Add(evaluation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvaluation), new { id = evaluation.Id }, evaluation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvaluation(Guid id, Evaluation evaluation)
        {
            if (id != evaluation.Id)
            {
                return BadRequest();
            }

            _context.Entry(evaluation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvaluationExists(id))
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
        public async Task<IActionResult> DeleteEvaluation(Guid id)
        {
            var evaluation = await _context.Evaluations.FindAsync(id);
            if (evaluation == null)
            {
                return NotFound();
            }

            _context.Evaluations.Remove(evaluation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EvaluationExists(Guid id)
        {
            return _context.Evaluations.Any(e => e.Id == id);
        }
    }
}