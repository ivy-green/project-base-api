using ProjectBase.Application.DTOs.Requests;
using ProjectBase.Application.DTOs.Responses;

namespace ProjectBase.Application.Services.AuthService
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO data, string ipAddress);
    }
}