using AleniaAPI.DTOs;

namespace AleniaAPI.Services
{
    public interface IMissionService
    {
        Task<IEnumerable<MissionDto>> GetAllMissionsAsync();
        Task<IEnumerable<MissionDto>> GetMissionsByEtablissementAsync(Guid etablissementId);
        Task<MissionDetailDto?> GetMissionByIdAsync(Guid id);
        Task<MissionDto?> CreateMissionAsync(CreateMissionDto createDto);
        Task<MissionDto?> UpdateMissionAsync(Guid id, UpdateMissionDto updateDto);
        Task<bool> DeleteMissionAsync(Guid id);
        Task<IEnumerable<MissionDto>> SearchMissionsAsync(string? poste, string? adresse, decimal? tauxMin, decimal? tauxMax); // Changé float en decimal
    }
}
