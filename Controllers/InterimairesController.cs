using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AleniaAPI.Services;
using AleniaAPI.DTOs;

namespace AleniaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterimairesController : ControllerBase
    {
        private readonly IInterimaireService _interimaireService;

        public InterimairesController(IInterimaireService interimaireService)
        {
            _interimaireService = interimaireService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InterimaireDto>>> GetInterimaires()
        {
            try
            {
                var interimaires = await _interimaireService.GetAllInterimairesAsync();
                return Ok(interimaires);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InterimaireDto>> GetInterimaire(Guid id)
        {
            try
            {
                var interimaire = await _interimaireService.GetInterimaireByIdAsync(id);
                if (interimaire == null)
                {
                    return NotFound(new { message = "Intérimaire non trouvé" });
                }

                return Ok(interimaire);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<InterimaireDto>> GetInterimaireByEmail(string email)
        {
            try
            {
                var interimaire = await _interimaireService.GetInterimaireByEmailAsync(email);
                if (interimaire == null)
                {
                    return NotFound(new { message = "Intérimaire non trouvé" });
                }

                return Ok(interimaire);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<InterimaireDto>> UpdateInterimaire(Guid id, UpdateInterimaireDto updateDto)
        {
            try
            {
                var interimaire = await _interimaireService.UpdateInterimaireAsync(id, updateDto);
                if (interimaire == null)
                {
                    return NotFound(new { message = "Intérimaire non trouvé" });
                }

                return Ok(interimaire);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteInterimaire(Guid id)
        {
            try
            {
                var success = await _interimaireService.DeleteInterimaireAsync(id);
                if (!success)
                {
                    return NotFound(new { message = "Intérimaire non trouvé" });
                }

                return Ok(new { message = "Intérimaire supprimé avec succès" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }
    }
}
