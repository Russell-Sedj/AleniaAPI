using AleniaAPI.Models;
using AleniaAPI.DTOs;

namespace AleniaAPI.Services
{    public interface IAuthService
    {
        Task<UtilisateurDto?> LoginAsync(LoginDto loginDto);
        Task<InterimaireDto?> RegisterInterimaireAsync(CreateInterimaireDto createDto);
        Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordDto changePasswordDto);
        Task<bool> EmailExistsAsync(string email);
        Task<Utilisateur?> GetUserByEmailAsync(string email);
        string GenerateJwtToken(Utilisateur user);
    }
}
