using AleniaAPI.DTOs;

namespace AleniaAPI.Services
{
    public interface IInterimaireService
    {
        Task<IEnumerable<InterimaireDto>> GetAllInterimairesAsync();
        Task<InterimaireDto?> GetInterimaireByIdAsync(Guid id);
        Task<InterimaireDto?> GetInterimaireByEmailAsync(string email);
        Task<InterimaireDto?> UpdateInterimaireAsync(Guid id, UpdateInterimaireDto updateDto);
        Task<bool> DeleteInterimaireAsync(Guid id);
    }
}
