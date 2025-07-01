using AleniaAPI.DTOs;

namespace AleniaAPI.Services
{
    public interface IMissionInterimaireService
    {
        Task<IEnumerable<MissionDto>> GetMissionsAccepteesForInterimaireAsync(Guid interimaireId);
        Task<bool> AccepterCandidatureAsync(Guid candidatureId);
    }
}