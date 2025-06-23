using Microsoft.EntityFrameworkCore;
using AleniaAPI.Data;
using AleniaAPI.Models;
using AleniaAPI.DTOs;

namespace AleniaAPI.Services
{
    public class MissionService : IMissionService
    {
        private readonly AleniaContext _context;

        public MissionService(AleniaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MissionDto>> GetAllMissionsAsync()
        {
            return await _context.Missions
                .Include(m => m.Etablissement)
                .Include(m => m.Candidatures)
                .Select(m => new MissionDto
                {
                    Id = m.Id,
                    EtablissementId = m.EtablissementId,
                    EtablissementNom = m.Etablissement != null ? m.Etablissement.Nom : "",
                    Poste = m.Poste,
                    Adresse = m.Adresse,
                    Description = m.Description,
                    TauxHoraire = m.TauxHoraire,
                    Horaires = m.Horaires,
                    DatePublication = m.DatePublication,
                    NombreCandidatures = m.Candidatures != null ? m.Candidatures.Count : 0
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<MissionDto>> GetMissionsByEtablissementAsync(Guid etablissementId)
        {
            return await _context.Missions
                .Where(m => m.EtablissementId == etablissementId)
                .Include(m => m.Etablissement)
                .Include(m => m.Candidatures)
                .Select(m => new MissionDto
                {
                    Id = m.Id,
                    EtablissementId = m.EtablissementId,
                    EtablissementNom = m.Etablissement != null ? m.Etablissement.Nom : "",
                    Poste = m.Poste,
                    Adresse = m.Adresse,
                    Description = m.Description,
                    TauxHoraire = m.TauxHoraire,
                    Horaires = m.Horaires,
                    DatePublication = m.DatePublication,
                    NombreCandidatures = m.Candidatures != null ? m.Candidatures.Count : 0
                })
                .ToListAsync();
        }

        public async Task<MissionDetailDto?> GetMissionByIdAsync(Guid id)
        {
            return await _context.Missions
                .Include(m => m.Etablissement)
                .Include(m => m.Candidatures)
                    .ThenInclude(c => c.Interimaire)
                .Where(m => m.Id == id)
                .Select(m => new MissionDetailDto
                {
                    Id = m.Id,
                    EtablissementId = m.EtablissementId,
                    EtablissementNom = m.Etablissement != null ? m.Etablissement.Nom : "",
                    Poste = m.Poste,
                    Adresse = m.Adresse,
                    Description = m.Description,
                    TauxHoraire = m.TauxHoraire,
                    Horaires = m.Horaires,
                    DatePublication = m.DatePublication,
                    NombreCandidatures = m.Candidatures != null ? m.Candidatures.Count : 0,
                    Candidatures = m.Candidatures!.Select(c => new CandidatureDto
                    {
                        Id = c.Id,
                        MissionId = c.MissionId,
                        MissionPoste = m.Poste,
                        MissionEtablissement = m.Etablissement != null ? m.Etablissement.Nom : "",
                        InterimaireId = c.InterimaireId,
                        InterimaireNom = c.Interimaire != null ? c.Interimaire.Nom : "",
                        InterimairePrenom = c.Interimaire != null ? c.Interimaire.Prenom : "",
                        Statut = c.Statut,
                        DateCandidature = c.DateCandidature,
                        HorairesChoisis = c.HorairesChoisis
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<MissionDto?> CreateMissionAsync(CreateMissionDto createDto)
        {
            var mission = new Mission
            {
                Id = Guid.NewGuid(),
                EtablissementId = createDto.EtablissementId,
                Poste = createDto.Poste,
                Adresse = createDto.Adresse,
                Description = createDto.Description,
                TauxHoraire = createDto.TauxHoraire,
                Horaires = createDto.Horaires,
                DatePublication = DateTime.UtcNow
            };

            _context.Missions.Add(mission);
            await _context.SaveChangesAsync();

            // Récupérer la mission avec l'établissement
            return await _context.Missions
                .Include(m => m.Etablissement)
                .Where(m => m.Id == mission.Id)
                .Select(m => new MissionDto
                {
                    Id = m.Id,
                    EtablissementId = m.EtablissementId,
                    EtablissementNom = m.Etablissement != null ? m.Etablissement.Nom : "",
                    Poste = m.Poste,
                    Adresse = m.Adresse,
                    Description = m.Description,
                    TauxHoraire = m.TauxHoraire,
                    Horaires = m.Horaires,
                    DatePublication = m.DatePublication,
                    NombreCandidatures = 0
                })
                .FirstOrDefaultAsync();
        }

        public async Task<MissionDto?> UpdateMissionAsync(Guid id, UpdateMissionDto updateDto)
        {
            var mission = await _context.Missions.FindAsync(id);
            if (mission == null) return null;

            if (!string.IsNullOrEmpty(updateDto.Poste))
                mission.Poste = updateDto.Poste;
            if (!string.IsNullOrEmpty(updateDto.Adresse))
                mission.Adresse = updateDto.Adresse;
            if (!string.IsNullOrEmpty(updateDto.Description))
                mission.Description = updateDto.Description;
            if (updateDto.TauxHoraire.HasValue)
                mission.TauxHoraire = updateDto.TauxHoraire.Value;
            if (updateDto.Horaires != null)
                mission.Horaires = updateDto.Horaires;

            await _context.SaveChangesAsync();

            return await _context.Missions
                .Include(m => m.Etablissement)
                .Include(m => m.Candidatures)
                .Where(m => m.Id == id)
                .Select(m => new MissionDto
                {
                    Id = m.Id,
                    EtablissementId = m.EtablissementId,
                    EtablissementNom = m.Etablissement != null ? m.Etablissement.Nom : "",
                    Poste = m.Poste,
                    Adresse = m.Adresse,
                    Description = m.Description,
                    TauxHoraire = m.TauxHoraire,
                    Horaires = m.Horaires,
                    DatePublication = m.DatePublication,
                    NombreCandidatures = m.Candidatures != null ? m.Candidatures.Count : 0
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteMissionAsync(Guid id)
        {
            var mission = await _context.Missions.FindAsync(id);
            if (mission == null) return false;

            _context.Missions.Remove(mission);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MissionDto>> SearchMissionsAsync(string? poste, string? adresse, float? tauxMin, float? tauxMax)
        {
            var query = _context.Missions
                .Include(m => m.Etablissement)
                .Include(m => m.Candidatures)
                .AsQueryable();

            if (!string.IsNullOrEmpty(poste))
                query = query.Where(m => m.Poste.Contains(poste));

            if (!string.IsNullOrEmpty(adresse))
                query = query.Where(m => m.Adresse.Contains(adresse));

            if (tauxMin.HasValue)
                query = query.Where(m => m.TauxHoraire >= tauxMin.Value);

            if (tauxMax.HasValue)
                query = query.Where(m => m.TauxHoraire <= tauxMax.Value);

            return await query
                .Select(m => new MissionDto
                {
                    Id = m.Id,
                    EtablissementId = m.EtablissementId,
                    EtablissementNom = m.Etablissement != null ? m.Etablissement.Nom : "",
                    Poste = m.Poste,
                    Adresse = m.Adresse,
                    Description = m.Description,
                    TauxHoraire = m.TauxHoraire,
                    Horaires = m.Horaires,
                    DatePublication = m.DatePublication,
                    NombreCandidatures = m.Candidatures != null ? m.Candidatures.Count : 0
                })
                .ToListAsync();
        }
    }
}
