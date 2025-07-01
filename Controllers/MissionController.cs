using Microsoft.AspNetCore.Mvc;
using AleniaAPI.Services;
using AleniaAPI.DTOs;

namespace AleniaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MissionsController : ControllerBase
    {
        private readonly IMissionService _missionService;

        public MissionsController(IMissionService missionService)
        {
            _missionService = missionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MissionDto>>> GetMissions()
        {
            try
            {
                var missions = await _missionService.GetAllMissionsAsync();
                return Ok(missions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MissionDetailDto>> GetMission(Guid id)
        {
            try
            {
                var mission = await _missionService.GetMissionByIdAsync(id);
                if (mission == null)
                {
                    return NotFound(new { message = "Mission non trouvée" });
                }

                return Ok(mission);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpGet("etablissement/{etablissementId}")]
        public async Task<ActionResult<IEnumerable<MissionDto>>> GetMissionsByEtablissement(Guid etablissementId)
        {
            try
            {
                var missions = await _missionService.GetMissionsByEtablissementAsync(etablissementId);
                return Ok(missions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<MissionDto>> CreateMission([FromBody] CreateMissionDto createDto)
        {
            try
            {
                var mission = await _missionService.CreateMissionAsync(createDto);
                if (mission == null)
                {
                    return BadRequest(new { message = "Impossible de créer la mission" });
                }

                return CreatedAtAction(nameof(GetMission), new { id = mission.Id }, mission);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MissionDto>> UpdateMission(Guid id, UpdateMissionDto updateDto)
        {
            try
            {
                var mission = await _missionService.UpdateMissionAsync(id, updateDto);
                if (mission == null)
                {
                    return NotFound(new { message = "Mission non trouvée" });
                }

                return Ok(mission);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMission(Guid id)
        {
            try
            {
                var success = await _missionService.DeleteMissionAsync(id);
                if (!success)
                {
                    return NotFound(new { message = "Mission non trouvée" });
                }

                return Ok(new { message = "Mission supprimée avec succès" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<MissionDto>>> SearchMissions(
            [FromQuery] string? poste = null,
            [FromQuery] string? adresse = null,
            [FromQuery] decimal? tauxMin = null,    // Changé float en decimal
            [FromQuery] decimal? tauxMax = null)    // Changé float en decimal
        {
            try
            {
                var missions = await _missionService.SearchMissionsAsync(poste, adresse, tauxMin, tauxMax);
                return Ok(missions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la recherche des missions", error = ex.Message });
            }
        }
    }
}