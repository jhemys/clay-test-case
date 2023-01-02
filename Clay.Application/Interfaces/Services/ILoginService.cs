using Clay.Application.DTOs;

namespace Clay.Application.Interfaces.Services
{
    public interface ILoginService
    {
        Task CreateLogin(LoginDTO login);
        Task<LoginDTO> GetByEmailAndPassword(string email, string password);
        Task ChangePassword(ChangePasswordDTO login);
        string GenerateToken(LoginDTO login);
    }
}
