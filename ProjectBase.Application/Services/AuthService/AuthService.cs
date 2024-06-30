using ProjectBase.Application.DTOs.Requests;
using ProjectBase.Application.DTOs.Responses;
using ProjectBase.Application.Services.Helpers;
using ProjectBase.Application.UnitOfWork;
using ProjectBase.Application.Utils;
using System.Security.Claims;
using ProjectBase.Insfracstructure.Services.Jwts;
using ProjectBase.Domain.Exceptions;
using ProjectBase.Domain.Entities;

namespace ProjectBase.Application.Services.AuthService
{
    public class AuthService : IAuthService
    {
        IUnitOfWork _unitOfWork;
        IJwtService _jwtService;
        IHashService _hashService;
        public AuthService(IUnitOfWork unitOfWork, IJwtService jwtService, IHashService hashService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _hashService = hashService;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO data, string ipAddress)
        {
            // check username if exixts
            User? user = await _unitOfWork.UserRepository.GetByCondition(x => x.Username == data.Username);
            if (user is null)
            {
                throw new Exception("User not found");
            }

            // check hash password
            var verifyUser = _hashService.VerifyPasswordHash(user.PasswordSalt, user.PasswordHash, data.Password);
            if (user.IsAccountBlock == true)
            {
                throw new UserBlockException();
            }

            // 
            var roles = user.UserRoles.Select(r => r.Role.RoleName).ToList() ?? new List<string>();
            //string userRole = string.Join(",", roles);
            List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, user.Username.ToString()),
                //new Claim(ClaimTypes.Role, userRole)
            ];
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtToken = GenerateTokens(claims, user.Username, ipAddress);

            await _unitOfWork.SaveChangesAsync();

            return new LoginResponseDTO
            {
                AccessToken = jwtToken,
                // RefreshToken = refreshToken,
                Roles = user.UserRoles.Select(r => r.Role).ToList().ProjectToDTOs<Role, RoleResponseDTO>()
            };
        }
        private string GenerateTokens(List<Claim> accessTokenClaims, string username, string ipAddress)
        {
            string accessToken = _jwtService.GenerateSecurityToken(accessTokenClaims);

            return accessToken;
        }
    }
}
