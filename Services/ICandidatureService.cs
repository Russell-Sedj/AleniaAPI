using AleniaAPI.DTOs;

namespace AleniaAPI.Services
{
    public interface ICandidatureService
    {
        Task<IEnumerable<CandidatureDto>> GetCandidaturesByInterimaireAsync(Guid interimaireId);
        Task<IEnumerable<CandidatureDto>> GetCandidaturesByMissionAsync(Guid missionId);
        Task<CandidatureDto?> GetCandidatureByIdAsync(Guid id);
        Task<CandidatureDto?> CreateCandidatureAsync(CreateCandidatureDto createDto);
        Task<CandidatureDto?> UpdateCandidatureStatutAsync(Guid id, UpdateCandidatureStatutDto updateDto);
        Task<bool> DeleteCandidatureAsync(Guid id);
        Task<bool> CandidatureExistsAsync(Guid missionId, Guid interimaireId);
    }
}
