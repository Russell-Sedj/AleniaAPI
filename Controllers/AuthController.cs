using Microsoft.AspNetCore.Mvc;
using AleniaAPI.Services;
using AleniaAPI.DTOs;

namespace AleniaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<object>> Login(LoginDto loginDto)
        {
            try
            {
                var user = await _authService.LoginAsync(loginDto);
                if (user == null)
                {
                    return Unauthorized(new { message = "Email ou mot de passe incorrect" });
                }

                // Récupérer l'utilisateur complet pour générer le token
                var userEntity = await _authService.GetUserByEmailAsync(loginDto.Email);
                if (userEntity == null)
                {
                    return Unauthorized(new { message = "Utilisateur non trouvé" });
                }

                var token = _authService.GenerateJwtToken(userEntity);

                return Ok(new
                {
                    user = user,
                    token = token,
                    message = "Connexion réussie"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<InterimaireDto>> Register(CreateInterimaireDto createDto)
        {
            try
            {
                if (createDto.MotDePasse != createDto.ConfirmMotDePasse)
                {
                    return BadRequest(new { message = "Les mots de passe ne correspondent pas" });
                }

                var interimaire = await _authService.RegisterInterimaireAsync(createDto);
                if (interimaire == null)
                {
                    return BadRequest(new { message = "L'email est déjà utilisé" });
                }

                return CreatedAtAction(nameof(Register), new { id = interimaire.Id }, interimaire);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }        }

        [HttpPost("login-etablissement")]
        public async Task<ActionResult<object>> LoginEtablissement(LoginEtablissementDto loginDto)
        {
            try
            {
                var etablissement = await _authService.LoginEtablissementAsync(loginDto);
                if (etablissement == null)
                {
                    return Unauthorized(new { message = "Email ou mot de passe incorrect" });
                }

                // Récupérer l'établissement complet pour générer le token
                var etablissementEntity = await _authService.GetUserByEmailAsync(loginDto.Email);
                if (etablissementEntity == null)
                {
                    return Unauthorized(new { message = "Établissement non trouvé" });
                }

                var token = _authService.GenerateJwtToken(etablissementEntity);

                return Ok(new
                {
                    etablissement = etablissement,
                    token = token,
                    message = "Connexion réussie"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpPost("register-etablissement")]
        public async Task<ActionResult<EtablissementDto>> RegisterEtablissement(CreateEtablissementDto createDto)
        {
            try
            {
                if (createDto.MotDePasse != createDto.ConfirmMotDePasse)
                {
                    return BadRequest(new { message = "Les mots de passe ne correspondent pas" });
                }

                var etablissement = await _authService.RegisterEtablissementAsync(createDto);
                if (etablissement == null)
                {
                    return BadRequest(new { message = "L'email est déjà utilisé" });
                }

                return CreatedAtAction(nameof(RegisterEtablissement), new { id = etablissement.Id }, etablissement);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            try
            {
                // Récupérer l'ID utilisateur depuis le token JWT (à implémenter avec l'authentification)
                // Pour l'instant, nous devrons passer l'ID dans la requête
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized(new { message = "Utilisateur non authentifié" });
                }

                if (!Guid.TryParse(userIdClaim.Value, out var userId))
                {
                    return BadRequest(new { message = "ID utilisateur invalide" });
                }

                if (changePasswordDto.NouveauMotDePasse != changePasswordDto.ConfirmNouveauMotDePasse)
                {
                    return BadRequest(new { message = "Les nouveaux mots de passe ne correspondent pas" });
                }

                var success = await _authService.ChangePasswordAsync(userId, changePasswordDto);
                if (!success)
                {
                    return BadRequest(new { message = "Ancien mot de passe incorrect" });
                }

                return Ok(new { message = "Mot de passe modifié avec succès" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }

        [HttpGet("check-email/{email}")]
        public async Task<ActionResult<object>> CheckEmail(string email)
        {
            try
            {
                var exists = await _authService.EmailExistsAsync(email);
                return Ok(new { exists = exists });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur", error = ex.Message });
            }
        }
    }
}
