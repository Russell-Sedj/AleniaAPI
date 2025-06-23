using Microsoft.EntityFrameworkCore;
using AleniaAPI.Data;
using AleniaAPI.Models;
using AleniaAPI.DTOs;

namespace AleniaAPI.Services
{
    public class CandidatureService : ICandidatureService
    {
        private readonly AleniaContext _context;

        public CandidatureService(AleniaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CandidatureDto>> GetCandidaturesByInterimaireAsync(Guid interimaireId)
        {
            return await _context.Candidatures
                .Include(c => c.Mission)
                    .ThenInclude(m => m.Etablissement)
                .Include(c => c.Interimaire)
                .Where(c => c.InterimaireId == interimaireId)
                .Select(c => new CandidatureDto
                {
                    Id = c.Id,
                    MissionId = c.MissionId,
                    MissionPoste = c.Mission != null ? c.Mission.Poste : "",
                    MissionEtablissement = c.Mission != null && c.Mission.Etablissement != null ? c.Mission.Etablissement.Nom : "",
                    InterimaireId = c.InterimaireId,
                    InterimaireNom = c.Interimaire != null ? c.Interimaire.Nom : "",
                    InterimairePrenom = c.Interimaire != null ? c.Interimaire.Prenom : "",
                    Statut = c.Statut,
                    DateCandidature = c.DateCandidature,
                    HorairesChoisis = c.HorairesChoisis
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CandidatureDto>> GetCandidaturesByMissionAsync(Guid missionId)
        {
            return await _context.Candidatures
                .Include(c => c.Mission)
                    .ThenInclude(m => m.Etablissement)
                .Include(c => c.Interimaire)
                .Where(c => c.MissionId == missionId)
                .Select(c => new CandidatureDto
                {
                    Id = c.Id,
                    MissionId = c.MissionId,
                    MissionPoste = c.Mission != null ? c.Mission.Poste : "",
                    MissionEtablissement = c.Mission != null && c.Mission.Etablissement != null ? c.Mission.Etablissement.Nom : "",
                    InterimaireId = c.InterimaireId,
                    InterimaireNom = c.Interimaire != null ? c.Interimaire.Nom : "",
                    InterimairePrenom = c.Interimaire != null ? c.Interimaire.Prenom : "",
                    Statut = c.Statut,
                    DateCandidature = c.DateCandidature,
                    HorairesChoisis = c.HorairesChoisis
                })
                .ToListAsync();
        }

        public async Task<CandidatureDto?> GetCandidatureByIdAsync(Guid id)
        {
            return await _context.Candidatures
                .Include(c => c.Mission)
                    .ThenInclude(m => m.Etablissement)
                .Include(c => c.Interimaire)
                .Where(c => c.Id == id)
                .Select(c => new CandidatureDto
                {
                    Id = c.Id,
                    MissionId = c.MissionId,
                    MissionPoste = c.Mission != null ? c.Mission.Poste : "",
                    MissionEtablissement = c.Mission != null && c.Mission.Etablissement != null ? c.Mission.Etablissement.Nom : "",
                    InterimaireId = c.InterimaireId,
                    InterimaireNom = c.Interimaire != null ? c.Interimaire.Nom : "",
                    InterimairePrenom = c.Interimaire != null ? c.Interimaire.Prenom : "",
                    Statut = c.Statut,
                    DateCandidature = c.DateCandidature,
                    HorairesChoisis = c.HorairesChoisis
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CandidatureDto?> CreateCandidatureAsync(CreateCandidatureDto createDto)
        {
            // Vérifier si la candidature existe déjà
            if (await CandidatureExistsAsync(createDto.MissionId, createDto.InterimaireId))
            {
                return null; // Candidature déjà existante
            }

            var candidature = new Candidature
            {
                Id = Guid.NewGuid(),
                MissionId = createDto.MissionId,
                InterimaireId = createDto.InterimaireId,
                Statut = "En cours", // Statut par défaut
                DateCandidature = DateTime.UtcNow,
                HorairesChoisis = createDto.HorairesChoisis
            };

            _context.Candidatures.Add(candidature);
            await _context.SaveChangesAsync();

            return await GetCandidatureByIdAsync(candidature.Id);
        }

        public async Task<CandidatureDto?> UpdateCandidatureStatutAsync(Guid id, UpdateCandidatureStatutDto updateDto)
        {
            var candidature = await _context.Candidatures.FindAsync(id);
            if (candidature == null) return null;

            // Valider le statut
            var statutsValides = new[] { "En cours", "Acceptée", "Refusée" };
            if (!statutsValides.Contains(updateDto.Statut))
            {
                return null;
            }

            candidature.Statut = updateDto.Statut;
            await _context.SaveChangesAsync();

            return await GetCandidatureByIdAsync(id);
        }

        public async Task<bool> DeleteCandidatureAsync(Guid id)
        {
            var candidature = await _context.Candidatures.FindAsync(id);
            if (candidature == null) return false;

            _context.Candidatures.Remove(candidature);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CandidatureExistsAsync(Guid missionId, Guid interimaireId)
        {
            return await _context.Candidatures
                .AnyAsync(c => c.MissionId == missionId && c.InterimaireId == interimaireId);
        }
    }
}
