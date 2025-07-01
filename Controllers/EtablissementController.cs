using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AleniaAPI.Services;
using AleniaAPI.DTOs;
using System.Security.Claims;

namespace AleniaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EtablissementController : ControllerBase
    {
        private readonly IEtablissementService _etablissementService;

        public EtablissementController(IEtablissementService etablissementService)
        {
            _etablissementService = etablissementService;
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentEtablissement()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Utilisateur non authentifié");
                }

                var etablissement = await _etablissementService.GetEtablissementByIdAsync(Guid.Parse(userId));
                if (etablissement == null)
                {
                    return NotFound("Établissement non trouvé");
                }

                return Ok(etablissement);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la récupération de l'établissement", error = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateEtablissement([FromBody] UpdateEtablissementDto updateDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("Utilisateur non authentifié");
                }

                var result = await _etablissementService.UpdateEtablissementAsync(Guid.Parse(userId), updateDto);
                if (result == null)
                {
                    return NotFound("Établissement non trouvé");
                }

                return Ok(new { message = "Établissement mis à jour avec succès", etablissement = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de la mise à jour", error = ex.Message });
            }
        }
    }
}