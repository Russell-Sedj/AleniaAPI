using Microsoft.AspNetCore.Mvc;
using AleniaAPI.Services;
using AleniaAPI.DTOs;

namespace AleniaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidaturesController : ControllerBase
    {
        private readonly ICandidatureService _candidatureService;

        public CandidaturesController(ICandidatureService candidatureService)
        {
            _candidatureService = candidatureService;
        }

        [HttpGet("interimaire/{interimaireId}")]
        public async Task<ActionResult<IEnumerable<CandidatureDto>>> GetCandidaturesByInterimaire(Guid interimaireId)
        {
            try
            {
                var candidatures = await _candidatureService.GetCandidaturesByInterimaireAsync(interimaireId);
                return Ok(candidatures);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpGet("mission/{missionId}")]
        public async Task<ActionResult<IEnumerable<CandidatureDto>>> GetCandidaturesByMission(Guid missionId)
        {
            try
            {
                var candidatures = await _candidatureService.GetCandidaturesByMissionAsync(missionId);
                return Ok(candidatures);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CandidatureDto>> GetCandidature(Guid id)
        {
            try
            {
                var candidature = await _candidatureService.GetCandidatureByIdAsync(id);
                if (candidature == null)
                {
                    return NotFound(new { message = "Candidature non trouvée" });
                }

                return Ok(candidature);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<CandidatureDto>> CreateCandidature(CreateCandidatureDto createDto)
        {
            try
            {
                var candidature = await _candidatureService.CreateCandidatureAsync(createDto);
                if (candidature == null)
                {
                    return BadRequest(new { message = "Vous avez déjà candidaté pour cette mission" });
                }

                return CreatedAtAction(nameof(GetCandidature), new { id = candidature.Id }, candidature);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpPut("{id}/statut")]
        public async Task<ActionResult<CandidatureDto>> UpdateCandidatureStatut(Guid id, UpdateCandidatureStatutDto updateDto)
        {
            try
            {
                var candidature = await _candidatureService.UpdateCandidatureStatutAsync(id, updateDto);
                if (candidature == null)
                {
                    return NotFound(new { message = "Candidature non trouvée ou statut invalide" });
                }

                return Ok(candidature);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCandidature(Guid id)
        {
            try
            {
                var success = await _candidatureService.DeleteCandidatureAsync(id);
                if (!success)
                {
                    return NotFound(new { message = "Candidature non trouvée" });
                }

                return Ok(new { message = "Candidature supprimée avec succès" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpGet("exists")]
        public async Task<ActionResult<object>> CheckCandidatureExists([FromQuery] Guid missionId, [FromQuery] Guid interimaireId)
        {
            try
            {
                var exists = await _candidatureService.CandidatureExistsAsync(missionId, interimaireId);
                return Ok(new { exists = exists });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }
    }
}
