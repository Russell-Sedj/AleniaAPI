using Microsoft.EntityFrameworkCore;
using AleniaAPI.Data;
using AleniaAPI.Models;
using AleniaAPI.DTOs;

namespace AleniaAPI.Services
{
    public class InterimaireService : IInterimaireService
    {
        private readonly AleniaContext _context;

        public InterimaireService(AleniaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InterimaireDto>> GetAllInterimairesAsync()
        {
            return await _context.Interimaires
                .Select(i => new InterimaireDto
                {
                    Id = i.Id,
                    Email = i.Email,
                    Nom = i.Nom,
                    Prenom = i.Prenom,
                    Adresse = i.Adresse,
                    Telephone = i.Telephone,
                    Competences = i.Competences,
                    Disponibilites = i.Disponibilites,
                    DateCreation = i.DateCreation
                })
                .ToListAsync();
        }

        public async Task<InterimaireDto?> GetInterimaireByIdAsync(Guid id)
        {
            return await _context.Interimaires
                .Where(i => i.Id == id)
                .Select(i => new InterimaireDto
                {
                    Id = i.Id,
                    Email = i.Email,
                    Nom = i.Nom,
                    Prenom = i.Prenom,
                    Adresse = i.Adresse,
                    Telephone = i.Telephone,
                    Competences = i.Competences,
                    Disponibilites = i.Disponibilites,
                    DateCreation = i.DateCreation
                })
                .FirstOrDefaultAsync();
        }

        public async Task<InterimaireDto?> GetInterimaireByEmailAsync(string email)
        {
            return await _context.Interimaires
                .Where(i => i.Email == email)
                .Select(i => new InterimaireDto
                {
                    Id = i.Id,
                    Email = i.Email,
                    Nom = i.Nom,
                    Prenom = i.Prenom,
                    Adresse = i.Adresse,
                    Telephone = i.Telephone,
                    Competences = i.Competences,
                    Disponibilites = i.Disponibilites,
                    DateCreation = i.DateCreation
                })
                .FirstOrDefaultAsync();
        }

        public async Task<InterimaireDto?> UpdateInterimaireAsync(Guid id, UpdateInterimaireDto updateDto)
        {
            var interimaire = await _context.Interimaires.FindAsync(id);
            if (interimaire == null) return null;

            if (!string.IsNullOrEmpty(updateDto.Nom))
                interimaire.Nom = updateDto.Nom;
            if (!string.IsNullOrEmpty(updateDto.Prenom))
                interimaire.Prenom = updateDto.Prenom;
            if (updateDto.Adresse != null)
                interimaire.Adresse = updateDto.Adresse;
            if (updateDto.Telephone != null)
                interimaire.Telephone = updateDto.Telephone;
            if (updateDto.Competences != null)
                interimaire.Competences = updateDto.Competences;
            if (updateDto.Disponibilites != null)
                interimaire.Disponibilites = updateDto.Disponibilites;

            await _context.SaveChangesAsync();

            return await GetInterimaireByIdAsync(id);
        }

        public async Task<bool> DeleteInterimaireAsync(Guid id)
        {
            var interimaire = await _context.Interimaires.FindAsync(id);
            if (interimaire == null) return false;

            _context.Interimaires.Remove(interimaire);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
