using Microsoft.EntityFrameworkCore;
using AleniaAPI.Data;
using AleniaAPI.Models;
using AleniaAPI.DTOs;

namespace AleniaAPI.Services
{
    public class MissionInterimaireService : IMissionInterimaireService
    {
        private readonly AleniaContext _context;

        public MissionInterimaireService(AleniaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MissionDto>> GetMissionsAccepteesForInterimaireAsync(Guid interimaireId)
        {
            return await _context.Candidatures
                .Include(c => c.Mission)
                    .ThenInclude(m => m.Etablissement)
                .Where(c => c.InterimaireId == interimaireId && c.Statut == "Acceptée")
                .Select(c => new MissionDto
                {
                    Id = c.Mission!.Id,
                    EtablissementId = c.Mission.EtablissementId,
                    EtablissementNom = c.Mission.Etablissement != null ? c.Mission.Etablissement.Nom : "",
                    Poste = c.Mission.Poste,
                    Adresse = c.Mission.Adresse,
                    Description = c.Mission.Description,
                    TauxHoraire = c.Mission.TauxHoraire,
                    Horaires = c.Mission.Horaires,
                    DatePublication = c.Mission.DatePublication,
                    NombreCandidatures = 0
                })
                .ToListAsync();
        }

        public async Task<bool> AccepterCandidatureAsync(Guid candidatureId)
        {
            var candidature = await _context.Candidatures
                .Include(c => c.Mission)
                .FirstOrDefaultAsync(c => c.Id == candidatureId);

            if (candidature == null) return false;

            candidature.Statut = "Acceptée";
            
            // Refuser automatiquement toutes les autres candidatures pour cette mission
            var autresCandidatures = await _context.Candidatures
                .Where(c => c.MissionId == candidature.MissionId && c.Id != candidatureId && c.Statut == "En cours")
                .ToListAsync();

            foreach (var autre in autresCandidatures)
            {
                autre.Statut = "Refusée";
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}