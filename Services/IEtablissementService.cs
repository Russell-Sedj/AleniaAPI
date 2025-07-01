using AleniaAPI.Data;
using AleniaAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AleniaAPI.Services
{
    public class EtablissementService : IEtablissementService
    {
        private readonly AleniaContext _context;

        public EtablissementService(AleniaContext context)
        {
            _context = context;
        }

        public async Task<EtablissementResponseDto?> GetEtablissementByIdAsync(Guid id)
        {
            var etablissement = await _context.Etablissements
                .FirstOrDefaultAsync(e => e.Id == id);

            if (etablissement == null)
                return null;

            return new EtablissementResponseDto
            {
                Id = etablissement.Id,
                Nom = etablissement.Nom,
                TypeEtablissement = etablissement.TypeEtablissement,
                Adresse = etablissement.Adresse,
                Telephone = etablissement.Telephone,
                Email = etablissement.Email,
                NumeroSiret = etablissement.NumeroSiret,
                Responsable = etablissement.Responsable,
                Description = etablissement.Description,
                DateInscription = DateTime.Now // Utilisez DateTime.Now en attendant d'ajouter la propriété
            };
        }

        public async Task<EtablissementResponseDto?> UpdateEtablissementAsync(Guid id, UpdateEtablissementDto updateDto)
        {
            var etablissement = await _context.Etablissements
                .FirstOrDefaultAsync(e => e.Id == id);

            if (etablissement == null)
                return null;

            // Mettre à jour les propriétés
            etablissement.Nom = updateDto.Nom ?? etablissement.Nom;
            etablissement.TypeEtablissement = updateDto.Type ?? etablissement.TypeEtablissement;
            etablissement.Adresse = updateDto.Adresse ?? etablissement.Adresse;
            etablissement.Telephone = updateDto.Telephone ?? etablissement.Telephone;
            etablissement.Email = updateDto.Email ?? etablissement.Email;
            etablissement.NumeroSiret = updateDto.Siret ?? etablissement.NumeroSiret;
            etablissement.Responsable = updateDto.Responsable ?? etablissement.Responsable;
            etablissement.Description = updateDto.Description ?? etablissement.Description;

            await _context.SaveChangesAsync();

            return new EtablissementResponseDto
            {
                Id = etablissement.Id,
                Nom = etablissement.Nom,
                TypeEtablissement = etablissement.TypeEtablissement,
                Adresse = etablissement.Adresse,
                Telephone = etablissement.Telephone,
                Email = etablissement.Email,
                NumeroSiret = etablissement.NumeroSiret,
                Responsable = etablissement.Responsable,
                Description = etablissement.Description,
                DateInscription = DateTime.Now // Temporaire
            };
        }
    }
}