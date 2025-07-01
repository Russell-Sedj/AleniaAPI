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
                    NombreCandidatures = m.Candidatures.Count
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
                    NombreCandidatures = m.Candidatures.Count
                })
                .ToListAsync();
        }

        public async Task<MissionDetailDto?> GetMissionByIdAsync(Guid id)
        {
            var mission = await _context.Missions
                .Include(m => m.Etablissement)
                .Include(m => m.Candidatures)
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            if (mission == null) return null;

            var candidatures = new List<CandidatureDto>();
            if (mission.Candidatures != null)
            {
                foreach (var candidature in mission.Candidatures)
                {
                    await _context.Entry(candidature)
                        .Reference(c => c.Interimaire)
                        .LoadAsync();

                    candidatures.Add(new CandidatureDto
                    {
                        Id = candidature.Id,
                        MissionId = candidature.MissionId,
                        MissionPoste = mission.Poste,
                        MissionEtablissement = mission.Etablissement?.Nom ?? "",
                        InterimaireId = candidature.InterimaireId,
                        InterimaireNom = candidature.Interimaire?.Nom ?? "",
                        InterimairePrenom = candidature.Interimaire?.Prenom ?? "",
                        Statut = candidature.Statut,
                        DateCandidature = candidature.DateCandidature,
                        HorairesChoisis = candidature.HorairesChoisis
                    });
                }
            }

            return new MissionDetailDto
            {
                Id = mission.Id,
                EtablissementId = mission.EtablissementId,
                EtablissementNom = mission.Etablissement?.Nom ?? "",
                Poste = mission.Poste,
                Adresse = mission.Adresse,
                Description = mission.Description,
                TauxHoraire = mission.TauxHoraire,
                Horaires = mission.Horaires,
                DatePublication = mission.DatePublication,
                NombreCandidatures = mission.Candidatures?.Count ?? 0,
                Candidatures = candidatures
            };
        }

        public async Task<MissionDto?> CreateMissionAsync(CreateMissionDto createDto)
        {
            // Vérifier que l'établissement existe
            var etablissementExiste = await _context.Etablissements
                .AnyAsync(e => e.Id == createDto.EtablissementId);

            if (!etablissementExiste)
            {
                throw new ArgumentException($"Aucun établissement trouvé avec l'ID: {createDto.EtablissementId}");
            }

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

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erreur lors de la sauvegarde de la mission: {ex.Message}", ex);
            }

            // Récupérer la mission avec l'établissement
            var result = await _context.Missions
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

            if (result == null)
            {
                throw new InvalidOperationException("La mission a été créée mais n'a pas pu être récupérée");
            }

            return result;
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
                    NombreCandidatures = m.Candidatures.Count
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

        public async Task<IEnumerable<MissionDto>> SearchMissionsAsync(string? poste, string? adresse, decimal? tauxMin, decimal? tauxMax)
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
                    NombreCandidatures = m.Candidatures.Count
                })
                .ToListAsync();
        }
    }
}
