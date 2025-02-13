using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AleniaAPI.Data;
using AleniaAPI.Models;

namespace AleniaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly AleniaContext _context;

        public MissionController(AleniaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mission>>> GetMissions()
        {
            return await _context.Missions.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Mission>> GetMission(Guid id)
        {
            var mission = await _context.Missions.FindAsync(id);

            if (mission == null)
            {
                return NotFound();
            }

            return mission;
        }

        [HttpPost]
        public async Task<ActionResult<Mission>> PostMission(Mission mission)
        {
            // Check if the EtablissementId is valid first
            var etablissement = await _context.Etablissements.FindAsync(mission.EtablissementId);
            if (etablissement == null)
            {
                return BadRequest("Etablissement not found");
            }

            // Mettre la meme adresse que etablissement si adresse est vide
            if (string.IsNullOrEmpty(mission.Adresse))
            {
                mission.Adresse = etablissement.Adresse;
            }

            // Définir la date de publication à la date du jour
            mission.DatePublication = DateTime.Now;

            _context.Missions.Add(mission);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMission), new { id = mission.Id }, mission);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMission(Guid id, Mission mission)
        {
            if (id != mission.Id)
            {
                return BadRequest();
            }

            var etablissement = await _context.Etablissements.FindAsync(mission.EtablissementId);
            if (etablissement == null)
            {
                return BadRequest("Etablissement not found");
            }

            // Mettre la meme adresse que etablissement si adresse est vide
            if (string.IsNullOrEmpty(mission.Adresse))
            {
                mission.Adresse = etablissement.Adresse;
            }

            _context.Entry(mission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MissionExists(id))
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
        public async Task<IActionResult> DeleteMission(Guid id)
        {
            var mission = await _context.Missions.FindAsync(id);
            if (mission == null)
            {
                return NotFound();
            }

            _context.Missions.Remove(mission);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MissionExists(Guid id)
        {
            return _context.Missions.Any(e => e.Id == id);
        }
    }
}