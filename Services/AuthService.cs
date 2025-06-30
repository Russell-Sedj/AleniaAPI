using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AleniaAPI.Data;
using AleniaAPI.Models;
using AleniaAPI.DTOs;

namespace AleniaAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AleniaContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AleniaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<UtilisateurDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Utilisateurs
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null || !VerifyPassword(loginDto.MotDePasse, user.MotDePasse))
            {
                return null;
            }

            return new UtilisateurDto
            {
                Id = user.Id,
                Email = user.Email,
                DateCreation = user.DateCreation
            };
        }

        public async Task<InterimaireDto?> RegisterInterimaireAsync(CreateInterimaireDto createDto)
        {
            if (await EmailExistsAsync(createDto.Email))
            {
                return null; // Email déjà utilisé
            }

            var interimaire = new Interimaire
            {
                Id = Guid.NewGuid(),
                Email = createDto.Email,
                MotDePasse = HashPassword(createDto.MotDePasse),
                Nom = createDto.Nom,
                Prenom = createDto.Prenom,
                Adresse = createDto.Adresse,
                Telephone = createDto.Telephone,
                Competences = createDto.Competences,
                Disponibilites = createDto.Disponibilites,
                DateCreation = DateTime.UtcNow
            };

            _context.Interimaires.Add(interimaire);
            await _context.SaveChangesAsync();

            return new InterimaireDto
            {
                Id = interimaire.Id,
                Email = interimaire.Email,
                Nom = interimaire.Nom,
                Prenom = interimaire.Prenom,
                Adresse = interimaire.Adresse,
                Telephone = interimaire.Telephone,
                Competences = interimaire.Competences,
                Disponibilites = interimaire.Disponibilites,
                DateCreation = interimaire.DateCreation
            };
        }

        public async Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordDto changePasswordDto)
        {
            var user = await _context.Utilisateurs.FindAsync(userId);
            if (user == null || !VerifyPassword(changePasswordDto.AncienMotDePasse, user.MotDePasse))
            {
                return false;
            }

            if (changePasswordDto.NouveauMotDePasse != changePasswordDto.ConfirmNouveauMotDePasse)
            {
                return false;
            }

            user.MotDePasse = HashPassword(changePasswordDto.NouveauMotDePasse);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<EtablissementDto?> LoginEtablissementAsync(LoginEtablissementDto loginDto)
        {
            var etablissement = await _context.Etablissements
                .FirstOrDefaultAsync(e => e.Email == loginDto.Email);

            if (etablissement == null || !VerifyPassword(loginDto.MotDePasse, etablissement.MotDePasse))
            {
                return null;
            }

            return new EtablissementDto
            {
                Id = etablissement.Id,
                Email = etablissement.Email,
                Nom = etablissement.Nom,
                Responsable = etablissement.Responsable,
                Adresse = etablissement.Adresse,
                Telephone = etablissement.Telephone,
                TypeEtablissement = etablissement.TypeEtablissement,
                NumeroSiret = etablissement.NumeroSiret,
                Description = etablissement.Description,
                DateCreation = etablissement.DateCreation
            };
        }

        public async Task<EtablissementDto?> RegisterEtablissementAsync(CreateEtablissementDto createDto)
        {
            if (await EmailExistsAsync(createDto.Email))
            {
                return null; // Email déjà utilisé
            }

            var etablissement = new Etablissement
            {
                Id = Guid.NewGuid(),
                Email = createDto.Email,
                MotDePasse = HashPassword(createDto.MotDePasse),
                Nom = createDto.Nom,
                Responsable = createDto.Responsable,
                Adresse = createDto.Adresse,
                Telephone = createDto.Telephone,
                TypeEtablissement = createDto.TypeEtablissement,
                NumeroSiret = createDto.NumeroSiret,
                Description = createDto.Description,
                DateCreation = DateTime.UtcNow
            };

            _context.Etablissements.Add(etablissement);
            await _context.SaveChangesAsync();

            return new EtablissementDto
            {
                Id = etablissement.Id,
                Email = etablissement.Email,
                Nom = etablissement.Nom,
                Responsable = etablissement.Responsable,
                Adresse = etablissement.Adresse,
                Telephone = etablissement.Telephone,
                TypeEtablissement = etablissement.TypeEtablissement,
                NumeroSiret = etablissement.NumeroSiret,
                Description = etablissement.Description,
                DateCreation = etablissement.DateCreation
            };
        }        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Utilisateurs.AnyAsync(u => u.Email == email);
        }

        public async Task<Utilisateur?> GetUserByEmailAsync(string email)
        {
            return await _context.Utilisateurs.FirstOrDefaultAsync(u => u.Email == email);
        }

        public string GenerateJwtToken(Utilisateur user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"] ?? "DefaultSecretKey_ChangeThis_InProduction_123456789";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.GetType().Name)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"] ?? "AleniaAPI",
                audience: jwtSettings["Audience"] ?? "AleniaAPI",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }
    }
}
